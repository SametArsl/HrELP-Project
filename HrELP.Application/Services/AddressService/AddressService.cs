using AutoMapper;
using HrELP.Application.Models.DTOs;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<Address> GetUserAddressAsync(int id)
        {
            Address address = await _addressRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            return address;
        }
    }
}
