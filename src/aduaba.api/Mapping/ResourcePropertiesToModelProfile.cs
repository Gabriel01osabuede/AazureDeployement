using aduaba.api.Resource;
using aduaba.api.Entities.ApplicationEntity;
using AutoMapper;

namespace aduaba.api.Mapping
{
    public class ResourcePropertiesToModelProfile : Profile
    {
        public ResourcePropertiesToModelProfile()
        {
            CreateMap<AddCategoryResource, Category>().ReverseMap();
            CreateMap<AddProductResource, Product>().ReverseMap();
            CreateMap<AddCartResource, Cart>();
            CreateMap<UpdateCart, Cart>();
        }
    }
}