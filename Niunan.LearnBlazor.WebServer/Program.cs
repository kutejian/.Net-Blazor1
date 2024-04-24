using Com.Ctrip.Framework.Apollo.Enums;
using Com.Ctrip.Framework.Apollo;
using LearnBlazorRepository.Repository;
using LearnBlazorRepository.Repository.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Utility;

namespace Niunan.LearnBlazor.WebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            //���apllo
            builder.Configuration.AddApollo(builder.Configuration.GetSection("apollo"))
                .AddNamespace("LearnBlazorNamespace", ConfigFileFormat.Json).AddDefault();

            //����
            ServiceConfigurator.ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            //ȫ���쳣

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}