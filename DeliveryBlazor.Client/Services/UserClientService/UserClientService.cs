using DeliveryBlazor.Shared.DataTransferObjects;
using DeliveryBlazor.Core.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using DeliveryBlazor.UseCase.Features.ApplicationUsers.Queries;
using DeliveryBlazor.UseCase.Features.ApplicationUsers.Commands;
using System.Text.Json;

namespace DeliveryBlazor.Client.Services.UserClientService
{
    public class UserClientService : IUserServices
    {
        private readonly HttpClient _httpClient;

        public UserClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> CreateUserAndClientAsync(ApplicationUserDto user, string? password = null)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                password = "Parola123!";
            }

            var command = new CreateApplicationUserCommand
            {
                UserName = user.Email, 
                Email = user.Email,
                Password = password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address ?? "",
                Role = user.Role
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7027/api/ApplicationUser", command);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to create user. Status Code: {response.StatusCode}, Error: {errorContent}");
            }
            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            var newIdString = body.GetProperty("id").GetString();
            return Guid.Parse(newIdString);
        }


        public async Task<List<ApplicationUserDto>> GetAllApplicationUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7027/api/ApplicationUser/list");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch users. Status Code: {response.StatusCode}");
            }
            var users = await response.Content.ReadFromJsonAsync<List<ApplicationUserDto>>();
            return users ?? new List<ApplicationUserDto>();
        }

        public async Task<Guid> DeleteUser(string id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7027/api/ApplicationUser/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete users. Status Code: {response.StatusCode}");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return Guid.Empty;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {responseContent}");

            if (string.IsNullOrWhiteSpace(responseContent))
            {
                throw new Exception("Response content is empty.");
            }

            return JsonSerializer.Deserialize<Guid>(responseContent);
        }


        public async Task UpdateUserAsync(string id, ApplicationUserDto user)
        {
            // Construiești comanda de update
            var command = new UpdateApplicationUserCommand
            {
                Id = id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Role = user.Role
            };

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7027/api/ApplicationUser/{id}", command);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to update user with ID {id}. Status Code: {response.StatusCode}, Error: {errorContent}");
            }
        }

        public async Task<ApplicationUserDto> GetUserById(string id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7027/api/ApplicationUser/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get user with ID {id}. Status Code: {response.StatusCode}");
            }
            var user = await response.Content.ReadFromJsonAsync<ApplicationUserDto>();
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            return user;
        }
    }
}
