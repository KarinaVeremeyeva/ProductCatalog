using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProductCatalog.BLL.Models;
using ProductCatalog.DAL.Entities;
using ProductСatalog.BLL.Models;

namespace ProductCatalog.BLL
{
    public class BusinessLogicProfile : Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<IdentityUser, UserModel>();
            CreateMap<IdentityRole, RoleModel>();
        }
    }
}
