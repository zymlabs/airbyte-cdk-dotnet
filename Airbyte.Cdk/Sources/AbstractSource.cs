using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using Airbyte.Cdk.Models;
using Airbyte.Cdk.Sources.Streams;
using Airbyte.Cdk.Sources.Streams.Http;
using Airbyte.Cdk.Sources.Utils;
using Type = Airbyte.Cdk.Models.Type;

namespace Airbyte.Cdk.Sources
{
    /// <summary>
    /// Abstract base class for an Airbyte Source. Consumers should implement any abstract methods
    /// in this class to create an Airbyte Specification compliant Source.
    /// </summary>
    public abstract class AbstractSource : Source
    {
        protected AirbyteLogger Logger { get; private set; }

        protected ChannelWriter<AirbyteMessage> Channel { get; private set; }

        protected JsonElement Config { get; private set; }

        protected JsonElement State { get; private set; }

        protected ConfiguredAirbyteCatalog Catalog { get; private set; }

        /// <summary>
        /// Check if a connection can be established
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config">The user-provided configuration as specified by the source's spec. This usually contains information required to check connection e.g. tokens, secrets and keys etc.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>A Dictionary of (boolean, error). If boolean is true, then the connection check is successful and we can connect to the underlying data
        /// source using the provided configuration.</returns>
        public abstract bool CheckConnection(AirbyteLogger logger, JsonElement config, out Exception exception);

        /// <summary>
        /// An array of the streams in this source connector.
        /// </summary>
        /// <param name="config">The user-provided configuration as specified by the source's spec. Any stream construction related operation should happen here.</param>
        /// <returns></returns>
        public abstract Stream[] Streams(JsonElement config);

        /// <summary>
        /// Source name
        /// </summary>
        public string Name
        {
            get => GetType().Name;
        }

        /// <summary>
        /// Implements the Discover operation from the Airbyte Specification. See https://docs.airbyte.io/architecture/airbyte-specification.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override AirbyteCatalog Discover(AirbyteLogger logger, JsonElement config)
            => new() { Streams = Streams(config).Select(x => x.AsAirbyteStream()).ToArray() };

        /// <summary>
        /// Implements the Check Connection operation from the Airbyte Specification. See https://docs.airbyte.io/architecture/airbyte-specification.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override AirbyteConnectionStatus Check(AirbyteLogger logger, JsonElement config)
        {
            try
            {
                return CheckConnection(logger, config, out var exc)
                    ? new AirbyteConnectionStatus { Status = Status.Succeeded }
                    : throw exc;
            }
            catch (Exception e)
            {
                return new AirbyteConnectionStatus { Status = Status.Failed, Message = e.Message };
            }
        }

        /// <summary>
        /// Implements the Read operation from the Airbyte Specification. See https://docs.airbyte.io/architecture/airbyte-specification.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="channel"></param>
        /// <param name="config"></param>
        /// <param name="catalog"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public override async Task Read(AirbyteLogger logger, ChannelWriter<AirbyteMessage> channel,
            JsonElement config,
            ConfiguredAirbyteCatalog catalog, JsonElement state)
        {
            // Set values
            Logger = logger;
            Channel = channel;
            Config = config;
            Catalog = catalog;
            State = state;

            logger.Info($"Starting syncing {Name}");
            var streamsInstances = Streams(config);
            foreach (var configuredStream in catalog.Streams)
            {
                var streamInstance = streamsInstances.FirstOrDefault(x => x.Name == configuredStream.Stream.Name);
                if (streamInstance == null)
                    throw new Exception(
                        $"The requested stream {configuredStream.Stream.Name} was not found in the source. Available streams: {streamsInstances.Select(x => x.Name)}");

                try
                {
                    await ReadStream(logger, streamInstance, configuredStream);
                }
                catch (Exception e)
                {
                    logger.Exception(e);
                    logger.Fatal($"Fatal exception occurred during ReadStream: {e.Message}");
                    throw;
                }
            }
        }

        private async Task ReadStream(AirbyteLogger logger, Stream streamInstance, ConfiguredAirbyteStream configuredStream)
        {
            if (Config.TryGetProperty("_page_size", out var pageSizeElement) &&
                streamInstance is HttpStream stream && pageSizeElement.TryGetInt32(out int pagesize))
            {
                Logger.Info($"Setting page size for {Name} to {pagesize}");
                stream.PageSize = pagesize;
            }

            long recordCount;
            long? recordLimit = Config.TryGetProperty("_limit", out var limitElement) && limitElement.TryGetInt64(out var limit) ? limit : null;
            Logger.Info($"Syncing stream: {streamInstance.Name}");

            var streamName = configuredStream.Stream.Name;
            if (State.TryGetProperty(streamName, out var stateElement))
                Logger.Info($"Setting state of {streamName} stream to {stateElement}");
            else
                stateElement = "{}".AsJsonElement();

            if (configuredStream.SyncMode == SyncMode.incremental && streamInstance.SupportsIncremental)
                recordCount = await ReadIncremental(logger, streamInstance, configuredStream, stateElement, recordLimit);
            else
                recordCount = await streamInstance.ReadRecords(logger, SyncMode.full_refresh, Channel, stateElement, recordLimit,
                    configuredStream.CursorField, null);

            Logger.Info($"Read {recordCount} records from {streamInstance.Name} stream");
        }

        private async Task<long> ReadIncremental(AirbyteLogger logger, Stream streamInstance, ConfiguredAirbyteStream configuredStream, JsonElement stateElement, long? recordLimit = null)
        {
            var slices = streamInstance.StreamSlices(SyncMode.incremental, configuredStream.CursorField, stateElement);
            return await streamInstance.ReadRecords(logger, SyncMode.incremental, Channel, stateElement, recordLimit, null, slices);
        }

        public static AirbyteMessage AsAirbyteMessage(string streamName, JsonElement data) => new()
        {
            Type = Type.Record,
            Record = new()
            {
                EmittedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Data = data,
                Stream = streamName
            }
        };
    }
}
