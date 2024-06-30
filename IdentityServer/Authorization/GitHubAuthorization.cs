using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using IdentityServer.Models.ResultModel;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using IdentityServer;
using IdentityServer.Models.Credential;
using IdentityServer.Authorization;
using IdentityServer.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using IdentityServer.Model;
using System.Security.Claims;
using Newtonsoft.Json.Linq;

namespace identityserver.Authorization
{
    public class GitHubAuthorization : IAuthorizationMethod
    {
        private const string AuthorizeUri = "https://github.com/login/oauth/authorize";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;
        private readonly ILocalStorageService _localStorage;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public GitHubAuthorization(IHttpClientFactory httpClientFactory, IOptions<GithubCredential> options
            , ILocalStorageService localStorage, CustomAuthenticationStateProvider authenticationStateProvider
            , UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _httpClientFactory = httpClientFactory;
            _clientId = options.Value.ClientId;
            _clientSecret = options.Value.ClientSecret;
            _redirectUri = options.Value.RedirectUri;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public string Authorize()
        {
            var scopes = new List<string>
            {
                "user","repo","gist"
            };

            var param = new Dictionary<string, string>
            {
                { "client_id", _clientId },
                { "redirect_uri", _redirectUri },
                { "scope", string.Join(',', scopes) },
                { "state", "123456" }
            };

            var requestUri = QueryHelpers.AddQueryString(AuthorizeUri, param);

            return requestUri;
        }
        //没有用了，直接用了Callback方法
        public async Task<ResultModel> Callback(string code, string state)
        {
            const string uri = "https://github.com/login/oauth/access_token";

            var param = new Dictionary<string, string>
            {
                ["client_id"] = _clientId,
                ["client_secret"] = _clientSecret,
                ["code"] = code,
                ["state"] = state
            };
            using var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.PostAsync(uri, new FormUrlEncodedContent(param));
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{response.StatusCode}, {responseContent}");
            }
            string accessToken = Convert.ToString(
                JsonConvert.DeserializeObject<dynamic>(responseContent)!.access_token);

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException($"accessToken is empty");
            }
            return new ResultModel() { AccessToken = accessToken };
        }

        public async  Task<ClaimsIdentity>  UserData(string accessToken)
        {
            const string uri = "https://api.github.com/user";

            using var httpClient = _httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Awesome-Octocat-App");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", accessToken);

            var response = httpClient.GetAsync(uri).Result;
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{response.StatusCode}, {responseContent}");
            }
            var gitHubUser = JsonConvert.DeserializeObject<GithubUserViewModel>(responseContent);

            var user = await _userManager.FindByEmailAsync(gitHubUser.Email);
            if (user == null)
            {
                // 如果用户不存在，则创建新用户
                user = new UserEntity
                {
                    UserName = gitHubUser.Email,
                    LastName = gitHubUser.Name,
                    FirstName = gitHubUser.Name,
                    Email = gitHubUser.Email,
                    AvatarUrl = gitHubUser.AvatarUrl,
                    UserPath = "" + Guid.NewGuid().ToString("N").Substring(0, 7), //生成随机路径
                    RegistrationTimestamp = DateTime.Now,
                    EmailConfirmed = true //默认激活邮箱
                };
                var result = await _userManager.CreateAsync(user);


                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "User").Wait();
                    var usermodel = _userManager.FindByNameAsync(user.Email).Result;
                    
                    return GithubLogin(usermodel); 
                }
            }
            return GithubLogin(user);
        }

        public  ClaimsIdentity GithubLogin(UserEntity user)
        {

            var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            var rols = _userManager.GetRolesAsync(user).Result;
            foreach (var role in rols)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return identity;
            //这行代码实现了用户登录的功能，使用了ASP.NET Core的身份验证中间件，将用户标识信息添加到当前HTTP上下文中，并指示用户已成功登录。
            //没有用问ai 还有没有别的办法
        }
    }
}
