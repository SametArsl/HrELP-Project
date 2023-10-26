using HrELP.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.AddressAPIService
{
    public interface IAddressAPIService
    {
        Task<List<string>> GetCitiesAsync();
        Task<List<string>> GetTownsByCityAsync(string city);
        Task<List<string>> GetDistrictsByTown(string town, string city);
        Task<List<string>> GetQuartersByDistrictAsync(string town, string city, string district);
    }
}
