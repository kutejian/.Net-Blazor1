using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IdentityServer.Models.Credential;
using IdentityServer.Models.ResultModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using IdentityServer.Authorization;

namespace identityserver.Authorization
{
    public class GoogleAuthorization : IAuthorizationMethod
    {
        private readonly string _redirectUri;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly IHttpClientFactory _httpClientFactory;

        public GoogleAuthorization(IOptions<GoogleCredential> options, IHttpClientFactory httpClientFactory)
        {
            _clientId = options.Value.ClientId;
            _clientSecret = options.Value.ClientSecret;
            _redirectUri = options.Value.RedirectUri;
            _httpClientFactory = httpClientFactory;
        }
        public string Authorize()
        {
            var uri = "https://accounts.google.com/o/oauth2/v2/auth";

            var param = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", _clientId },
                { "redirect_uri",  _redirectUri},
                { "scope", "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile" },
                { "state", "123456" },
                { "access_type", "offline" }
            };

            var requestUri = QueryHelpers.AddQueryString(uri, param);

            return requestUri;
        }

        public async Task<ResultModel> Callback(string code, string state)
        {
            var uri = "https://oauth2.googleapis.com/token";

            var param = new Dictionary<string, string>
            {
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri",  _redirectUri},
                {"state", state}
            };

            using var httpClient = _httpClientFactory.CreateClient();

            var requestContent = new FormUrlEncodedContent(param);

            var response = await httpClient.PostAsync(uri, requestContent);
            var responseContent = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(responseContent);
            }

            string accessToken = Convert.ToString(responseContent!.access_token);

            (string userId, string email) userInfo = DecodeJwt(Convert.ToString(responseContent.id_token));

            if (string.IsNullOrEmpty(accessToken))
            {
                throw new Exception("accessToken is empty");
            }



            return new ResultModel() { AccessToken = accessToken, Email = userInfo.email, UserId = userInfo.userId };
        }

        public async Task<GoogleUserInfo> UserData<GoogleUserInfo>(string accessToken)
        {
            const string uri = "https://www.googleapis.com/oauth2/v2/userinfo";

            using var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            var userInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(
                await response.Content.ReadAsStringAsync());

            return userInfo;
        }
        //JWT驗證
        private (string userId, string email) DecodeJwt(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwt = handler.ReadJwtToken(idToken); //JWT驗證物件
                if (jwt == null)
                    throw new ArgumentException("Invalid JWT token");

                var idTokens = idToken.Split('.');
                var header = idTokens[0];
                var payload = idTokens[1];
                var signature = idTokens[2];

                var payloadDecoded = Base64UrlEncoder.Decode(payload);

                var payloadJson = JObject.Parse(payloadDecoded);

                return (
                    userId: Convert.ToString(payloadJson["sub"]),
                    email: Convert.ToString(payloadJson["email"])
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"msg:{ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
