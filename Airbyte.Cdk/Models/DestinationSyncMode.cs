using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Destination Sync Mode
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))] 
    public enum DestinationSyncMode
    {
        [EnumMember(Value = "append")]
        Append,
        
        [EnumMember(Value = "overwrite")]
        Overwrite,
        
        [EnumMember(Value = "append_dedup")]
        AppendDedup
    }
}