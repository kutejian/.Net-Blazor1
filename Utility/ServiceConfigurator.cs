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
using System.Reflection;

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
            //
            services.AddSingleton<Category>();
            services.AddSingleton<Product>();
            services.AddSingleton<SqlSugarHelper>();
            services.AddSingleton<FluentValidator<Category>>();
            // 添加 Ant Design
            services.AddAntDesign();

            //注册数据验证
            services.AddFluentValidationValidators();

            //添加数据库配置文件
            services.AddSingleton(new SqlSugarHelper(configuration)); // 注册数据库配置

            services.AddMediatR(cgh =>
            {
                cgh.RegisterServicesFromAssemblyContaining<ProductAdd>();
            });
        }

        //添加   注册验证器
        public static void AddFluentValidationValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Category>, CategoryValidation>();
        }
    }
}