using Microsoft.AspNetCore.Mvc;
using LearnBlazorDto.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Niunan.LearnBlazor.WebServer.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityServer.Model;
using Blazored.LocalStorage;
using IdentityServer.Models.ViewModels;
using identityserver.Authorization;
using System.Security.Principal;
using IdentityServer.Model.Credential;
using System.Configuration;
using IdentityServer.Models.Credential;
using Microsoft.Extensions.Options;


namespace Niunan.LearnBlazor.WebServer.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class AuthorizeController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        public GitHubAuthorization _gitHubAuthorization;
        public GoogleAuthorization _googleAuthorization;
        public ILocalStorageService _localStorage;
        private readonly JwtSettings _jwtSettings;
        public AuthorizeController(GitHubAuthorization gItHubAuthorization, GoogleAuthorization 
            googleAuthorization, UserManager<UserEntity> userManager,
            ILocalStorageService localStorage , IOptions<JwtSettings> jwtSettings)
        {
            _gitHubAuthorization = gItHubAuthorization;
            _googleAuthorization = googleAuthorization;
            _userManager = userManager;
            _localStorage = localStorage;
            _jwtSettings = jwtSettings.Value;
        }
        [HttpPost]
        //防止跨域请求            
        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(model.Email).Result;
                if (user != null && _userManager.CheckPasswordAsync(user, model.Password).Result
                    && _userManager.IsEmailConfirmedAsync(user).Result)
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

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtSecurityKey));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expiry = DateTime.Now.AddDays(_jwtSettings.JwtExpiryInDays);

                    var token = new JwtSecurityToken(
                                _jwtSettings.JwtIssuer,
                                _jwtSettings.JwtAudience,
                                identity.Claims,
                                expires: expiry,
                                signingCredentials: creds
                            );


                    HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));

                    return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                return Unauthorized("Invalid login attempt.");
            }
            return View();
        }
        [HttpGet]
        public IActionResult GitHubAuthorize()
        {
            string url = _gitHubAuthorization.Authorize();
            return Ok(url);
        }
        [HttpPost]
        public IActionResult GitHubUserData([FromBody] string accessToken)
        {

            var identity = _gitHubAuthorization.UserData(accessToken).Result;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtSecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(_jwtSettings.JwtExpiryInDays);

            var token = new JwtSecurityToken(
                        _jwtSettings.JwtIssuer,
                        _jwtSettings.JwtAudience,
                        identity.Claims,
                        expires: expiry,
                        signingCredentials: creds
                    );

            HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));

            return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        public IActionResult GoogleAuthorize()
        {
            string url = _googleAuthorization.Authorize();
            return Redirect(url);
        }

        //根据 GitHub给的coed 生成一个token 并且储层到了Cookies里 根据cookies里拿
        public IActionResult GoogleCallback(string code, string state)
        {
            var result = _googleAuthorization.Callback(code, state).Result;
            HttpContext.Response.Cookies.Append(CookieTypeNames.GoogleAccessToken, result.AccessToken);
            HttpContext.Response.Cookies.Append(CookieTypeNames.GoogleUserId, result.UserId);
            HttpContext.Response.Cookies.Append(CookieTypeNames.GoogleEmail, result.Email);

            return RedirectToAction("GoogleUserData", "Authorize");
        }

        /// <summary>
        /// 用户已登录 获取到了 GitHub上传来的Token  
        /// </summary>
        /// <returns></returns>
        public IActionResult GoogleUserData()
        {
            if (!HttpContext.Request.Cookies.TryGetValue(CookieTypeNames.GoogleAccessToken, out var accessToken))
            {
                throw new Exception("accessToken is empty");
            }
            if (!HttpContext.Request.Cookies.TryGetValue(CookieTypeNames.GoogleUserId, out var _))
            {
                throw new Exception("google userId is empty");
            }
            var githubUserView = _googleAuthorization.UserData<GoogleUserViewModel>(accessToken).Result;

            return View(githubUserView);
        }
    }
}