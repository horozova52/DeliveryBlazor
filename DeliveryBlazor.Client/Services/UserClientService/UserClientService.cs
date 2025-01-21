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

        public async Task<Guid> CreateUserAndClientAsync(ApplicationUser user)
        {
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7027/api/ApplicationUser", user);

            if (!result.IsSuccessStatusCode)
                throw new Exception($"Failed to create user. Status Code: {result.StatusCode}");

            var userId = await result.Content.ReadFromJsonAsync<Guid>();
            return userId;
        }

        public async Task<List<ApplicationUser>> GetAllApplicationUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7027/api/ApplicationUser/list");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch users. Status Code: {response.StatusCode}");
            }
            var users = await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();
            return users ?? new List<ApplicationUser>();
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

        public async Task UpdateUserAsync(string id, ApplicationUser user)
        {
            // Asigură-te că ID-ul în comandă corespunde
            user.Id = id;

            // Setează UserName dacă nu este deja setat
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                // Setează UserName la o valoare implicită sau obține-l dintr-o altă sursă
                user.UserName = "defaultUserName"; // Înlocuiește cu valoarea corectă
            }

            // Logare date trimise
            Console.WriteLine($"Updating user with ID: {id}");
            Console.WriteLine($"User Data: {JsonSerializer.Serialize(user)}");

            // Trimitere cerere PUT către API
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7027/api/ApplicationUser/{id}", user);

            // Verificare succes cerere
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Content: {errorContent}");
                throw new Exception($"Failed to update user with ID {id}. Status Code: {response.StatusCode}, Error: {errorContent}");
            }
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7027/api/ApplicationUser/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get user with ID {id}. Status Code: {response.StatusCode}");
            }
            var user = await response.Content.ReadFromJsonAsync<ApplicationUser>();
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            return user;
        }
    }
}
