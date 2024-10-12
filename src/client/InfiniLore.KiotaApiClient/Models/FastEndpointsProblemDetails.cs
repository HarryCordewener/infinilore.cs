// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System;
namespace InfiniLore.KiotaApiClient.Models
{
    /// <summary>
    /// RFC7807 compatible problem details/ error response class. this can be used by configuring startup like so:    app.UseFastEndpoints(x =&gt; x.Errors.ResponseBuilder = ProblemDetails.ResponseBuilder);
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class FastEndpointsProblemDetails : ApiException, IParsable
    {
        /// <summary>the details of the error</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Detail { get; set; }
#nullable restore
#else
        public string Detail { get; set; }
#endif
        /// <summary>The errors property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails_Error>? Errors { get; set; }
#nullable restore
#else
        public List<global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails_Error> Errors { get; set; }
#endif
        /// <summary>The instance property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Instance { get; set; }
#nullable restore
#else
        public string Instance { get; set; }
#endif
        /// <summary>The primary error message.</summary>
        public override string Message { get => base.Message; }
        /// <summary>The status property</summary>
        public int? Status { get; set; }
        /// <summary>The title property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Title { get; set; }
#nullable restore
#else
        public string Title { get; set; }
#endif
        /// <summary>The traceId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TraceId { get; set; }
#nullable restore
#else
        public string TraceId { get; set; }
#endif
        /// <summary>The type property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Type { get; set; }
#nullable restore
#else
        public string Type { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails"/> and sets the default values.
        /// </summary>
        public FastEndpointsProblemDetails()
        {
            Instance = "/api/route";
            Title = "One or more validation errors occurred.";
            TraceId = "0HMPNHL0JHL76:00000001";
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "detail", n => { Detail = n.GetStringValue(); } },
                { "errors", n => { Errors = n.GetCollectionOfObjectValues<global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails_Error>(global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails_Error.CreateFromDiscriminatorValue)?.AsList(); } },
                { "instance", n => { Instance = n.GetStringValue(); } },
                { "status", n => { Status = n.GetIntValue(); } },
                { "title", n => { Title = n.GetStringValue(); } },
                { "traceId", n => { TraceId = n.GetStringValue(); } },
                { "type", n => { Type = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("detail", Detail);
            writer.WriteCollectionOfObjectValues<global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails_Error>("errors", Errors);
            writer.WriteStringValue("instance", Instance);
            writer.WriteIntValue("status", Status);
            writer.WriteStringValue("title", Title);
            writer.WriteStringValue("traceId", TraceId);
            writer.WriteStringValue("type", Type);
        }
    }
}
#pragma warning restore CS0618
