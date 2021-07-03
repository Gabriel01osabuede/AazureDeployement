using aduaba.api.Resource;
using aduaba.data.Entities.ApplicationEntity;
using AutoMapper;

namespace aduaba.api.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();
            CreateMap<Product, ProductResource>();
            // CreateMap<Cart, CartResource>();
        }
    }
}