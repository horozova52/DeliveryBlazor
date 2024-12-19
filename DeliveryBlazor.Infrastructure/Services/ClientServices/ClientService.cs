
using DeliveryBlazor.Shared.DataTransferObjects;
using DeliveryBlazor.Core.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace DeliveryBlazor.Infrastructure.Services.ClientService
{
    public class ClientService : IClientServices
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //get all couriers

        public async Task<List<ApplicationUserDto>> GetAllClientsAsync()
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
