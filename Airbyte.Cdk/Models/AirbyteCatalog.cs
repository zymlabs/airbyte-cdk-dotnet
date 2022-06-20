using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Airbyte Catalog
    /// </summary>
    public class AirbyteCatalog
    {
        /// <summary>
        /// Streams
        /// </summary>
        [JsonPropertyName("streams")]
        public AirbyteStream[] Streams { get; set; }
    }
}