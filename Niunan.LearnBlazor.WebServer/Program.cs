using Utility;
using Niunan.LearnBlazor.WebServer.Data;
using Microsoft.EntityFrameworkCore;
using GZY.Quartz.MUI.EFContext;
using GZY.Quartz.MUI.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components;
using IdentityServer;
using EmailServer.Model;
using EmailServer.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

namespace Niunan.LearnBlazor.WebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpClient();
            //注册跟控制器一样 前端获取后端数据的方式只能写在这个类里 
            builder.Services.AddSingleton<CategoryData>();
            builder.Services.AddSingleton<ProductData>();

            //添加QuartzUI图形化
            builder.Services.AddQuartzUI();


            builder.Services.AddQuartzClassJobs();
            //配置
            ServiceConfigurator.ConfigureServices(builder.Services, builder.Configuration);
            //登录配置
            ServiceIdentityConfigurator.IdentityConfigureServices(builder.Services, builder.Configuration);

            // 添加Email服务
            builder.Services.Configure<EmaliSendConfig>(builder.Configuration.GetSection("EmaliSendConfig"));
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<AuthService, AuthService>();


            builder.Services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp =>
                (ServerAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>()
            );


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();


            app.UseRouting();
            app.UseQuartz();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}