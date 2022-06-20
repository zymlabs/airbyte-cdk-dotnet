using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Log Level
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))] 
    public enum Level
    {
        [EnumMember(Value = "FATAL")]
        Fatal,
        
        [EnumMember(Value = "ERROR")]
        Error,
        
        [EnumMember(Value = "WARN")]
        Warn,
        
        [EnumMember(Value = "INFO")]
        Info,
        
        [EnumMember(Value = "DEBUG")]
        Debug,
        
        [EnumMember(Value = "TRACE")]
        Trace
    }
}