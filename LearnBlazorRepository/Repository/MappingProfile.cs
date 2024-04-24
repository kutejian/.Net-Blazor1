using AutoMapper;
using LearnBlazorDto.Models;
using LearnBlazorEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}