using AutoMapper;
using ProductCatalog.Web.DTOs;
using ProductCatalog.Web.ViewModels;

namespace ProductCatalog.Web
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();
            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<UpdateCategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<LoginDto, LoginViewModel>().ReverseMap();
        }
    }
}
