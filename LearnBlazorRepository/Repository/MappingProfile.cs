using AutoMapper;
using LearnBlazorDto.Models;
using LearnBlazorEntity.Models;
using IdentityServer.Model;
using LearnBlazorDto.Models.Account;

namespace LearnBlazorRepository.Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryEntity>();
            CreateMap<CategoryEntity, Category>();
            CreateMap<Product, ProductEntity>();
            CreateMap<ProductEntity, Product>();
            CreateMap<ProductImage, ProductImageEntity>();
            CreateMap<ProductImageEntity, ProductImage>();
            CreateMap<UserRegisterModel, IdentityServer.Model.UserEntity>();
            CreateMap<UserLoginModel, IdentityServer.Model.UserEntity>();

            CreateMap<UserRegisterModel, UserEntity>().ForMember(user => user.UserName,
               opt => opt.MapFrom(model => model.Email));
        }
    }
}