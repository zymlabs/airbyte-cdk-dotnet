#nullable enable
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Airbyte.Cdk.Models
{
    /// <summary>
    /// Connector Specification
    /// </summary>
    public class ConnectorSpecification
    {
        /// <summary>
        /// Documentation URL
        /// </summary>
        [JsonPropertyName("documentationUrl")]
        public string? DocumentationUrl { get; set; }

        /// <summary>
        /// Changelog URL
        /// </summary>
        [JsonPropertyName("changelogUrl")]
        public string? ChangelogUrl { get; set; }

        /// <summary>
        /// ConnectorDefinition specific blob. Must be a valid JSON string.
        /// </summary>
        [JsonPropertyName("connectionSpecification")]
        public JsonDocument? ConnectionSpecification { get; set; }

        /// <summary>
        /// If the connector supports incremental mode or not.
        /// </summary>
        [JsonPropertyName("supportsIncremental")]
        [Obsolete("Specified by the individual streams instead")]
        public bool? SupportsIncremental { get; set; }

        /// <summary>
        /// If the connector supports normalization or not.
        /// </summary>
        [JsonPropertyName("supportsNormalization")]
        public bool? SupportsNormalization { get; set; }

        /// <summary>
        /// If the connector supports DBT or not.
        /// </summary>
        [JsonPropertyName("supportsDBT")]
        public bool? SupportsDbt { get; set; }

        /// <summary>
        /// List of destination sync modes supported by the connector
        /// </summary>
        [JsonPropertyName("supported_destination_sync_modes")] // Why did they not follow conventions?
        public DestinationSyncMode[]? SupportedDestinationSyncModes { get; set; }

        /// <summary>
        /// Auth specification for connector
        /// </summary>
        [JsonPropertyName("authSpecification")]
        [Obsolete("Switching to advanced_auth instead")]
        public AuthSpecification? AuthSpecification { get; set; }
    }
}