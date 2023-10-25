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
        List<string> GetCitiesAsync();
        List<string> GetTownsByCityAsync(string city);
        List<string> GetDistrictsByTown(string town, string city);
        List<string> GetQuartersByDistrictAsync(string town, string city, string district);
    }
}
