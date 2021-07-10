using aduaba.api.Entities.ApplicationEntity;

namespace aduaba.api.Models.Communication
{
    public class AddressResponse : BaseResponse
    {
        public Address address { get; set; }
        
        
        public AddressResponse(bool success, string message, Address address) : base(success,message)
        {
            this.address = address;
        }
        public AddressResponse(Address address)  : this(true,string.Empty,address){}
        public AddressResponse(string message) : this(false,message,null){}
        
    }
}