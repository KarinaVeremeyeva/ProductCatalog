using AutoMapper;
using ProductCatalog.API.DTOs;
using ProductCatalog.BLL.Models;

namespace ProductCatalog.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductModel, ProductDto>().ReverseMap();
            CreateMap<CategoryModel, CategoryDto>().ReverseMap();
        }
    }
}
