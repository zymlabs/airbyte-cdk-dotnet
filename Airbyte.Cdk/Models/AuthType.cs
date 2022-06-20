using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Auth Type
    /// </summary>
    [JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]  // This custom converter was placed in a system namespace.
    public enum AuthType
    {
        [EnumMember(Value = "oauth2_0")]
        OAuth2
    }
}