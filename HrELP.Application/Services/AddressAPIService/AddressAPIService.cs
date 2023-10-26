using HrELP.Application.Services.AddressAPIService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.AddressAPIService
{
    public class AddressAPIService : IAddressAPIService
    {
        private async Task<List<string>> GetAllData(string action)
        {
            List<string> data = new List<string>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://turkeyaddressapi.azurewebsites.net/api/Address/");
                var response = client.GetAsync($"{action}");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var readData = readTask.Result;
                    data = JsonConvert.DeserializeObject<List<string>>(readData);
                }
            }
            return data;
        }
        public async Task<List<string>> GetCitiesAsync()
        {
            return await GetAllData("GetCities");
        }

        public async Task<List<string>> GetDistrictsByTown(string town, string city)
        {
            return await GetAllData($"GetDistricts?town={town}&city={city}");
        }

        public async Task<List<string>> GetQuartersByDistrictAsync(string town, string city, string district)
        {
            return await GetAllData($"GetQuarters?district={district}&town={town}&city={city}");
        }

        public async Task<List<string>> GetTownsByCityAsync(string city)
        {
            return await GetAllData($"GetTowns?city={city}");
        }
    }
}