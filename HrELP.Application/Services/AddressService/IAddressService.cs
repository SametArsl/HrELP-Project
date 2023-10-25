using HrELP.Application.Models.DTOs;
using HrELP.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.AddressService
{
    public interface IAddressService
    {
        Task<Address> GetUserAddressAsync(int id);
    }
}
