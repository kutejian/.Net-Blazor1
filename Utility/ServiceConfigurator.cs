using FluentValidation;
using LearnBlazorDto.Models;
using LearnBlazorDto.ModelsFluentValidation;
using LearnBlazorRepository;
using LearnBlazorRepository.Repository;
using LearnBlazorRepository.Repository.Interface;
using LearnBlazorServerMediator.CategoryMediator;
using LearnBlazorServerMediator.ProductMediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;


namespace Utility
{
    public static class ServiceConfigurator
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 注册 AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // 注册其他服务
            services.AddSingleton<IProductRepository, Niunan.LearnBlazor.WebServer.Repository.Implement.ProductRepository>();
            services.AddSingleton<ICategoryRepository, Niunan.LearnBlazor.WebServer.Repository.Implement.CategoryRepository>();

            services.AddSingleton<Category>();
            services.AddSingleton<Product>();
            services.AddSingleton<SqlSugarHelper>();

            //返回类型
            services.AddSingleton<CategoryOperationResponse>();
            services.AddSingleton<ProductOperationResponse>();
            //验证器
            services.AddSingleton<FluentValidatorCategory<Category>>();
            services.AddSingleton<FluentValidatorProduct<Product>>();


            //错误日志 文件路径在 Niunan.LearnBlazor.WebServer\bin\Debug\net7.0\Logs
            services.AddLogging(builder => builder.AddFileLogger());

            // 添加 Ant Design
            services.AddAntDesign();
            // 添加 BootstrapBlazor
            services.AddBootstrapBlazor();

            //注册数据验证
            services.AddFluentValidationValidators();

            //添加数据库配置文件
            services.AddSingleton(new SqlSugarHelper(configuration)); // 注册数据库配置

            //添加中间件
            services.AddMediatR(cgh =>
            {
                cgh.RegisterServicesFromAssemblyContaining<ProductAdd>();
            });
        }

        //添加   注册验证器
        public static void AddFluentValidationValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Category>, CategoryValidation>();
            services.AddTransient<IValidator<Product>, ProductValidation>();
        }
    }
}