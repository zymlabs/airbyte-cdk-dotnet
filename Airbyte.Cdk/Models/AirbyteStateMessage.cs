using System.Text.Json;
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Airbyte State Message
    /// </summary>
    public class AirbyteStateMessage
    {
        /// <summary>
        /// The state data
        /// </summary>
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public JsonElement Data { get; set; }
    }
}