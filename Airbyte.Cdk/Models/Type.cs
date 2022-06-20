using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Message Type
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))] 
    public enum Type
    {
        [EnumMember(Value = "RECORD")]
        Record,
        
        [EnumMember(Value = "STATE")]
        State,
        
        [EnumMember(Value = "LOG")]
        Log,
        
        [EnumMember(Value = "SPEC")]
        Spec,
        
        [EnumMember(Value = "CONNECTION_STATUS")]
        ConnectionStatus,
        
        [EnumMember(Value = "CATALOG")]
        Catalog
    }
}