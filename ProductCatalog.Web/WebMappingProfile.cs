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
            CreateMap<UpdateProductDto, ProductViewModel>().ReverseMap();
            CreateMap<UpdateProductViewModel, UpdateProductDto>();
            CreateMap<ProductDto, UpdateProductViewModel>()
                .ForMember(dest => dest.CategoryId,opt => opt.MapFrom(src => src.Category.Id));
            CreateMap<FilterProductViewModel, FilterProductDto>();
            CreateMap<UpdateCategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<LoginDto, LoginViewModel>().ReverseMap();
            CreateMap<UserDto, UserViewModel>();
        }
    }
}
