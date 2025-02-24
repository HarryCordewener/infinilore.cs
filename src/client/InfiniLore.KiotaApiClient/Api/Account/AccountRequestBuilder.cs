// <auto-generated/>
#pragma warning disable CS0618
using InfiniLore.KiotaApiClient.Api.Account.Identity;
using InfiniLore.KiotaApiClient.Api.Account.Jwt;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace InfiniLore.KiotaApiClient.Api.Account
{
    /// <summary>
    /// Builds and executes requests for operations under \api\account
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AccountRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The identity property</summary>
        public global::InfiniLore.KiotaApiClient.Api.Account.Identity.IdentityRequestBuilder Identity
        {
            get => new global::InfiniLore.KiotaApiClient.Api.Account.Identity.IdentityRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The jwt property</summary>
        public global::InfiniLore.KiotaApiClient.Api.Account.Jwt.JwtRequestBuilder Jwt
        {
            get => new global::InfiniLore.KiotaApiClient.Api.Account.Jwt.JwtRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::InfiniLore.KiotaApiClient.Api.Account.AccountRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AccountRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api/account", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::InfiniLore.KiotaApiClient.Api.Account.AccountRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AccountRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api/account", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
