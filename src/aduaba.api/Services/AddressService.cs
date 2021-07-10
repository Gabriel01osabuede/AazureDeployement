using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using aduaba.api.Interface;
using aduaba.api.Models.Communication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace aduaba.api.Services
{
    public class AddressService : IAddressInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(ApplicationDbContext context, IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<AddressResponse> AddToAddress(String UserId,Address address)
        {
            try
            {
                address.UserId = UserId;
                await _context.addresses.AddAsync(address);
                await _unitOfWork.CompleteAsync();

                return new AddressResponse(address);
            }
            catch (Exception ex)
            {
                return new AddressResponse($"An error occurred while saving the Address : {ex.Message}");
            }
        }

        public async Task<AddressResponse> DeleteAddressAsync(string Id)
        {
            var existingAddress = await _context.addresses.FindAsync(Id);

            if (existingAddress == null)
                return new AddressResponse("Address Not Found");

            try
            {
                _context.addresses.Remove(existingAddress);
                await _unitOfWork.CompleteAsync();

                return new AddressResponse(existingAddress);
            }
            catch (Exception ex)
            {
                return new AddressResponse(ex.Message);
            }
        }

        public Task<List<Address>> GetAddress(string UserId)
        {
            if (UserId == null) throw new ArgumentNullException(nameof(UserId));
            else
            {
                var GetUserAddress = _context.addresses.Where(s => s.UserId == UserId)
                                .ToListAsync();
                if (!(GetUserAddress == null))
                {
                    return GetUserAddress;
                }

                else { return null; }

            }
        }

        public async Task<AddressResponse> UpdateAddressAsync(string Id, Address address)
        {
            var existingAddress = await _context.addresses.FindAsync(Id);
            if (existingAddress == null)
                return new AddressResponse("Address Not Found");
            if (!(string.IsNullOrEmpty(address.HouseNumber)))
            {
                existingAddress.HouseNumber = address.HouseNumber;
            }
            if (!(string.IsNullOrEmpty(address.state)))
            {
                existingAddress.state = address.state;
            }
            if (!(string.IsNullOrEmpty(address.country)))
            {
                existingAddress.country = address.country;
            }
            if (!(string.IsNullOrEmpty(address.PhoneNumber)))
            {
                existingAddress.PhoneNumber = address.PhoneNumber;
            }

            try
            {
                _context.addresses.Update(existingAddress);
                await _unitOfWork.CompleteAsync();

                return new AddressResponse(existingAddress);
            }
            catch (Exception ex)
            {
                return new AddressResponse($"An error occurred when Updating the Address : {ex.Message}");
            }
        }

    }

}

