using AutoMapper;
using ProductCatalog.BLL.Models;
using ProductCatalog.DAL.Entities;

namespace ProductCatalog.BLL
{
    public class BusinessLogicProfile : Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
        }
    }
}
