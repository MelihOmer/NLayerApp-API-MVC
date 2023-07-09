using AutoMapper;
using NLayer.Core.Dtos.CategoryDtos;
using NLayer.Core.Dtos.ProductDtos;
using NLayer.Core.Dtos.ProductWithCategoryDtos;
using NLayer.Core.Entities;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductWithCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryWithProducts>().ReverseMap();
        }
    }
}
