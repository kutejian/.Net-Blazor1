using Blazored.LocalStorage;
using identityserver.Authorization;
using IdentityServer.Data;
using IdentityServer.Model;
using IdentityServer.Model.Credential;
using IdentityServer.Models.Credential;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityServer
{
    public static class ServiceIdentityConfigurator
    {
        public static void IdentityConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("SqlServerIdentity").GetSection("DefaultConnection").Value;
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // 将Identity服务添加到DI容器中
            services.AddIdentity<UserEntity, IdentityRole>()
                // 将Entity Framework存储添加到Identity
                .AddEntityFrameworkStores<ApplicationDbContext>()
                // 添加默认令牌提供程序
                .AddDefaultTokenProviders();


            services.AddSession(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
            });
            // 添加GitHub OAuth2认证
            services.Configure<GoogleCredential>(configuration.GetSection("Credentials:Google"));
            services.Configure<GithubCredential>(configuration.GetSection("Credentials:Github"));
            services.AddScoped<GitHubAuthorization>();
            services.AddSingleton<GoogleAuthorization>();
            services.AddHttpContextAccessor();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.JwtIssuer,
                        ValidAudience = jwtSettings.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtSecurityKey))
                    };
                });



            services.AddScoped<CustomAuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();



            services.Configure<DataProtectionTokenProviderOptions>(
                options => options.TokenLifespan = TimeSpan.FromMinutes(1));

            services.Configure<IdentityOptions>(options =>
            {

                //RequireDigit, RequireLowercase, RequireNonAlphanumeric, RequireUppercase, RequiredLength, RequiredUniqueChars分别设置密码的要求
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                    
                // 设置用户登录需要确认电子邮件
                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // AllowedUserNameCharacters: 允许用作用户名的字符集。在此示例中，允许使用的字符包括大小写字母、数字以及一些特殊字符
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                //RequireUniqueEmail: 确定是否要求唯一的电子邮件地址用于用户账户。在此处设置为 false，表示不要求每个用户的电子邮件地址是唯一的。
                options.User.RequireUniqueEmail = true;

            });

            services.ConfigureApplicationCookie(options =>
            {
                // ConfigureApplicationCookie()配置了应用程序的cookie认证。
                //HttpOnly设置为true以确保cookie仅通过HTTP发送。
                //ExpireTimeSpan设置为5分钟后过期。

                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                //LoginPath和AccessDeniedPath分别指定了登录页面和访问被拒绝页面的路径。
                //SlidingExpiration设置为true以启用滑动过期。
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });



        }

    }
}
