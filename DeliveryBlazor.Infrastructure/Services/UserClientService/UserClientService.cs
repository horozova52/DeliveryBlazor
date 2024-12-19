using DeliveryBlazor.Shared.DataTransferObjects;
using DeliveryBlazor.Core.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DeliveryBlazor.Infrastructure.Services.UserClientService
{
    public class UserClientService : IUserServices
    {
        private readonly HttpClient _httpClient;

        public UserClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> CreateUserAndClientAsync(ApplicationUserDto userDto)
        {
            // Apelează endpoint-ul pentru a crea utilizatorul și clientul asociat
            var result = await _httpClient.PostAsJsonAsync("api/ApplicationUser", userDto);

            if (!result.IsSuccessStatusCode)
                throw new Exception($"Failed to create user. Status Code: {result.StatusCode}");

            var userId = await result.Content.ReadFromJsonAsync<Guid>();
            return userId;
        }



        //get all aplication users

        public async Task<List<ApplicationUserDto>> GetAllApplicationUsersAsync()
        {
            // Utilizează HttpClient pentru a apela endpoint-ul GetAllUsers
            var response = await _httpClient.GetAsync("api/ApplicationUser/list");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch users. Status Code: {response.StatusCode}");
            }
            var users = await response.Content.ReadFromJsonAsync<List<ApplicationUserDto>>();
            return users ?? new List<ApplicationUserDto>();
        }

        //delete 
        public async Task<Guid> DeleteUser(string id)
        {
            var response = await _httpClient.DeleteAsync("api/ApplicationUser/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete users. Status Code: {response.StatusCode}");
            }
            
            return await response.Content.ReadFromJsonAsync<Guid>();
        }




    }
}
