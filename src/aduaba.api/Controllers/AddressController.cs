using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using aduaba.api.Extensions;
using aduaba.api.Interface;
using aduaba.api.Resource;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace aduaba.api.Controllers
{
    [Authorize]
    [ApiController]
    public class AddressController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAddressInterface _addressService;

        public AddressController(IAddressInterface addressService, UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _addressService = addressService;
        }

        [HttpGet]
        [Route("api/[controller]/GetUserAddress")]
        public async Task<List<ShowAddressResource>> GetAllUserAddress()
        {
            ShowAddressResource view = default;
            List<ShowAddressResource> AddressListItems = new List<ShowAddressResource>();
            var CustomerEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Customer = await _userManager.FindByEmailAsync(CustomerEmail);

            var existingAddress = await _addressService.GetAddress(Customer.Id);
            foreach (var item in existingAddress)
            {

                view = new ShowAddressResource
                {

                    UserName = item.User.FirstName + " " + item.User.LastName,
                    Street = item.Street,
                    HouseNumber = item.HouseNumber,
                    localGovernmentArea = item.localGovernmentArea,
                    State = item.state,
                    Country = item.country,
                    PhoneNumber = item.PhoneNumber,
                    addressId = item.addressId
                };
                AddressListItems.Add(view);
            }
            return AddressListItems;
        }

        [HttpPost]
        [Route("api/[controller]/PostUserAddress")]
        public async Task<IActionResult> AddItemToAddress([FromBody] AddAdrressResource address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                var CustomerEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Customer = await _userManager.FindByEmailAsync(CustomerEmail);
                var GetCustomer = Customer.Id;
                var mapAddress = _mapper.Map<AddAdrressResource, Address>(address);
                await _addressService.AddToAddress(GetCustomer, mapAddress);

                return Ok(address);
            }
        }

        [HttpPut]
        [Route("/api/[controller]/UpdateUserAddress")]
        public async Task<IActionResult> UpdateUserAddress([FromQuery] string AddressId, [FromBody] AddAdrressResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var Address = _mapper.Map<AddAdrressResource, Address>(model);
            var result = await _addressService.UpdateAddressAsync(AddressId, Address);

            if (!result.success)
                return BadRequest(result.message);

            var addressResource = _mapper.Map<Address, ShowAddressResource>(result.address);
            return Ok(addressResource);
        }

        [HttpDelete]
        [Route("/api/[controller]/RemoveUserAddress")]
        public async Task<IActionResult> RemoveCartItem([FromQuery] string addressId)
        {
            var deleteAddressAsync = await _addressService.DeleteAddressAsync(addressId);

            if (!deleteAddressAsync.success)
                return BadRequest(deleteAddressAsync.message);

            var cartResource = _mapper.Map<Address, ShowAddressResource>(deleteAddressAsync.address);
            return Ok(cartResource);
        }

    }
}