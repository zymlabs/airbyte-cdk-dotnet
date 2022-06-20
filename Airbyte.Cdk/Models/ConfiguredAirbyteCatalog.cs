using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Configured Airbyte Catalog
    /// </summary>
    public class ConfiguredAirbyteCatalog
    {
        /// <summary>
        /// Streams
        /// </summary>
        [JsonPropertyName("streams")]
        public ConfiguredAirbyteStream[] Streams { get; set; }
    }
}