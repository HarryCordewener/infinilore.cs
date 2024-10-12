// <auto-generated/>
#pragma warning disable CS0618
using InfiniLore.KiotaApiClient.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace InfiniLore.KiotaApiClient.Api.Account.Jwt.TokensCreate
{
    /// <summary>
    /// Builds and executes requests for operations under \api\account\jwt\tokens-create
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class TokensCreateRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::InfiniLore.KiotaApiClient.Api.Account.Jwt.TokensCreate.TokensCreateRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TokensCreateRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api/account/jwt/tokens-create", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::InfiniLore.KiotaApiClient.Api.Account.Jwt.TokensCreate.TokensCreateRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TokensCreateRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api/account/jwt/tokens-create", rawUrl)
        {
        }
        /// <returns>A <see cref="global::InfiniLore.KiotaApiClient.Models.InfiniLoreServerAPIModelsJwtResponse"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails">When receiving a 400 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::InfiniLore.KiotaApiClient.Models.InfiniLoreServerAPIModelsJwtResponse?> PostAsync(global::InfiniLore.KiotaApiClient.Models.InfiniLoreServerAPIControllersAccountJWTCreateJwtCreateTokensRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::InfiniLore.KiotaApiClient.Models.InfiniLoreServerAPIModelsJwtResponse> PostAsync(global::InfiniLore.KiotaApiClient.Models.InfiniLoreServerAPIControllersAccountJWTCreateJwtCreateTokensRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "400", global::InfiniLore.KiotaApiClient.Models.FastEndpointsProblemDetails.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::InfiniLore.KiotaApiClient.Models.InfiniLoreServerAPIModelsJwtResponse>(requestInfo, global::InfiniLore.KiotaApiClient.Models.InfiniLoreServerAPIModelsJwtResponse.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::InfiniLore.KiotaApiClient.Models.InfiniLoreServerAPIControllersAccountJWTCreateJwtCreateTokensRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::InfiniLore.KiotaApiClient.Models.InfiniLoreServerAPIControllersAccountJWTCreateJwtCreateTokensRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.POST, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::InfiniLore.KiotaApiClient.Api.Account.Jwt.TokensCreate.TokensCreateRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::InfiniLore.KiotaApiClient.Api.Account.Jwt.TokensCreate.TokensCreateRequestBuilder WithUrl(string rawUrl)
        {
            return new global::InfiniLore.KiotaApiClient.Api.Account.Jwt.TokensCreate.TokensCreateRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
#pragma warning restore CS0618
