using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Airbyte Log Message
    /// </summary>
    public class AirbyteLogMessage
    {
        /// <summary>
        /// The type of logging
        /// </summary>
        [JsonPropertyName("level")]
        public Level Level { get; set; }

        /// <summary>
        /// The log message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}