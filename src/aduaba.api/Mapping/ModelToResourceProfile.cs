using aduaba.api.Resource;
using aduaba.api.Entities.ApplicationEntity;
using AutoMapper;

namespace aduaba.api.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>().ReverseMap();
            CreateMap<Product, ProductResource>().ReverseMap();
            CreateMap<Cart, ShowCartResource>();
            // CreateMap<Cart, CartResource>();
        }
    }
}