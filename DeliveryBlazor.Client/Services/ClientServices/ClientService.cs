
using DeliveryBlazor.Shared.DataTransferObjects;
using DeliveryBlazor.Core.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace DeliveryBlazor.Client.Services.ClientService
{
    public class ClientService : IClientServices
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //get all couriers

        public async Task<List<ApplicationUser>> GetAllClientsAsync()
        {
            // Utilizează HttpClient pentru a apela endpoint-ul GetAllUsers
            var response = await _httpClient.GetAsync("https://localhost:7027/api/Clients");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch users. Status Code: {response.StatusCode}");
            }

            var users = await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();
            return users ?? new List<ApplicationUser>();
        }
        public async Task DeleteClientAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7027/api/Client/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete courier with ID {id}. Status Code: {response.StatusCode}");
            }
        }
    }
}
