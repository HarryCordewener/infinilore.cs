// <auto-generated/>
#pragma warning disable CS0618
using InfiniLore.KiotaApiClient.Api.LoreScopes.Seed;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace InfiniLore.KiotaApiClient.Api.LoreScopes
{
    /// <summary>
    /// Builds and executes requests for operations under \api\lore-scopes
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.18.0")]
    public partial class LoreScopesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The seed property</summary>
        public global::InfiniLore.KiotaApiClient.Api.LoreScopes.Seed.SeedRequestBuilder Seed
        {
            get => new global::InfiniLore.KiotaApiClient.Api.LoreScopes.Seed.SeedRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::InfiniLore.KiotaApiClient.Api.LoreScopes.LoreScopesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public LoreScopesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api/lore-scopes", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::InfiniLore.KiotaApiClient.Api.LoreScopes.LoreScopesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public LoreScopesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api/lore-scopes", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
