using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Sync Mode
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))] 
    public enum SyncMode
    {
        [EnumMember(Value = "full_refresh")]
        full_refresh,
        
        [EnumMember(Value = "incremental")]
        incremental
    }
}