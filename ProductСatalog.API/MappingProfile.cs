using AutoMapper;
using ProductCatalog.API.DTOs;
using ProductCatalog.BLL.Models;
using ProductСatalog.BLL.Models;

namespace ProductCatalog.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductModel, ProductDto>().ReverseMap();
            CreateMap<CategoryModel, CategoryDto>().ReverseMap();
            CreateMap<UpdateProductDto, ProductModel>();
            CreateMap<UpdateCategoryDto, CategoryModel>();
            CreateMap<UserModel, UserDto>();
            CreateMap<CreateUserDto, UserModel>();
            CreateMap<RoleModel, RoleDto>();
            CreateMap<FilterProductsModel, FilterProductsDto>().ReverseMap();
        }
    }
}
