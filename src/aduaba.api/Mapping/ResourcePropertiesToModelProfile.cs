using aduaba.api.Resource;
using aduaba.data.Entities.ApplicationEntity;
using AutoMapper;

namespace aduaba.api.Mapping
{
    public class ResourcePropertiesToModelProfile : Profile
    {
        public ResourcePropertiesToModelProfile()
        {
            CreateMap<AddCategoryResource, Category>();
            CreateMap<AddProductResource, Product>();
            // CreateMap<CartResource, Cart>();
            // CreateMap<UpdateCartResource, Cart>();
            
        }
    }
}