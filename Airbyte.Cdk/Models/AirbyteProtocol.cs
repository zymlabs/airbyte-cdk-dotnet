#nullable enable
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Airbyte Protocol
    /// </summary>
    public class AirbyteProtocol
    {
        /// <summary>
        /// Airbyte Message
        /// </summary>
        [JsonPropertyName("airbyte_message")]
        public AirbyteMessage? AirbyteMessage { get; set; }

        /// <summary>
        /// Airbyte Configured Catalog
        /// </summary>
        [JsonPropertyName("configured_airbyte_catalog")]
        public ConfiguredAirbyteCatalog? ConfiguredAirbyteCatalog { get; set; }
    }
}