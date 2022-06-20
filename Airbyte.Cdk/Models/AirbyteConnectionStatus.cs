#nullable enable
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Airbyte Connection Status
    /// </summary>
    public class AirbyteConnectionStatus
    {
        /// <summary>
        /// Connection Status
        /// </summary>
        [JsonPropertyName("status")]
        public Status Status { get; set; }

        /// <summary>
        /// Connection Message
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}