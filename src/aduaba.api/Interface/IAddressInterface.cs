using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using aduaba.api.Models.Communication;

namespace aduaba.api.Interface
{
    public interface IAddressInterface
    {
       Task<List<Address>> GetAddress(string UserId);
       Task<AddressResponse> UpdateAddressAsync(string Id, Address address);
       Task<AddressResponse> DeleteAddressAsync(string Id);
       Task<AddressResponse> AddToAddress(string UserId,Address address);
    }
}