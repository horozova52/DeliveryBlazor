
using DeliveryBlazor.Shared.DataTransferObjects;
using DeliveryBlazor.Core.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace DeliveryBlazor.Client.Services.CourierService
{
    public class CourierService : ICourierService
    {
        private readonly HttpClient _httpClient;

        public CourierService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //get all couriers

        public async Task<List<ApplicationUserDto>> GetAllCouriersAsync()
        {
            // Utilizează HttpClient pentru a apela endpoint-ul GetAllUsers
            var response = await _httpClient.GetAsync("api/Couriers");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch users. Status Code: {response.StatusCode}");
            }

            var users = await response.Content.ReadFromJsonAsync<List<ApplicationUserDto>>();
            return users ?? new List<ApplicationUserDto>();
        }
    }
}
