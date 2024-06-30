using Blazored.LocalStorage;
using IdentityServer;
using LearnBlazorDto.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;
using IdentityServer.Model;
using System.Net.Http.Headers;
using System.Text.Json;



namespace Niunan.LearnBlazor.WebServer.Data
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient Http;
        public AuthService(HttpClient httpClient,
            CustomAuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage,
            HttpClient http)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            Http = http;
        }
        public async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return token;
        }
        //普通登录
        public async Task<LoginResult> Login(UserLoginModel loginModel)
        {
            
            Http.BaseAddress = new Uri("https://localhost:5229/");
            var response = await Http.PostAsJsonAsync("/Authorize/login", loginModel);
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync(CookieTypeNames.AuthenticationUserAccessToken, loginResult.Token);
            _authenticationStateProvider.MarkUserAsAuthenticated(loginResult.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }
        //GitHub验证登录 
        public async Task<HttpResponseMessage> GithubLogin()
        {

            Http.BaseAddress = new Uri("https://localhost:5229/");
            var response = await Http.GetAsync("/Authorize/GitHubAuthorize");
            return response;
        }
        //GitHub验证登录回调 获取token储存到localStorage
        public async Task<LoginResult> GithubTokenLogin(string accessToken)
        {
            Http.BaseAddress = new Uri("https://localhost:5229/");
            var response = await Http.PostAsJsonAsync("/Authorize/GitHubUserData", accessToken);

            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync(CookieTypeNames.GithubAccessToken, loginResult.Token);
            _authenticationStateProvider.MarkUserAsAuthenticated(loginResult.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            return loginResult;
        }

        public async Task Logout()
        {

            var possibleKeys = new List<string>
            {
                CookieTypeNames.AuthenticationUserAccessToken,
                CookieTypeNames.GoogleAccessToken,
                CookieTypeNames.GoogleUserId,
                CookieTypeNames.GoogleEmail,
                CookieTypeNames.GithubAccessToken
            };
            var savedToken = "";
            foreach (var key in possibleKeys)
            {
                savedToken = await _localStorage.GetItemAsync<string>(key);
                if (!string.IsNullOrWhiteSpace(savedToken))
                {
                    break;
                }
            }

            await _localStorage.RemoveItemAsync(savedToken);
            _authenticationStateProvider.MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
    
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string Token { get; set; }
    }
}
