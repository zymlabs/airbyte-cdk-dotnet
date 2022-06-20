using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Status
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Status
    {
        [EnumMember(Value = "SUCCEEDED")]
        Succeeded,
        
        [EnumMember(Value = "FAILED")]
        Failed
    }
}