
using DeliveryBlazor.Shared.DataTransferObjects;
using DeliveryBlazor.Core.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using DeliveryBlazor.UseCase.Features.Couriers.Commands;
using MediatR;

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

        public async Task<List<ApplicationUser>> GetAllCouriersAsync()
        {
            // Utilizează HttpClient pentru a apela endpoint-ul GetAllUsers
            var response = await _httpClient.GetAsync("https://localhost:7027/api/Couriers");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch users. Status Code: {response.StatusCode}");
            }

            var users = await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();
            return users ?? new List<ApplicationUser>();
        }
        public async Task DeleteCourierAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7027/api/Courier/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete courier with ID {id}. Status Code: {response.StatusCode}");
            }
        }
    }
}
