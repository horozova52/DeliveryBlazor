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
            // Apelează endpoint-ul pentru a crea utilizatorul și clientul asociat
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7027/api/ApplicationUser", user);

            if (!result.IsSuccessStatusCode)
                throw new Exception($"Failed to create user. Status Code: {result.StatusCode}");

            var userId = await result.Content.ReadFromJsonAsync<Guid>();
            return userId;
        }



        //get all aplication users

        public async Task<List<ApplicationUser>> GetAllApplicationUsersAsync()
        {
            // Utilizează HttpClient pentru a apela endpoint-ul GetAllUsers
            var response = await _httpClient.GetAsync("https://localhost:7027/api/ApplicationUser/list");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch users. Status Code: {response.StatusCode}");
            }
            var users = await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();
            return users ?? new List<ApplicationUser>();
        }

        //delete 
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



        //update
        public async Task UpdateUserAsync(string id, ApplicationUser user)
        {
            // Убедимся, что ID в команде совпадает
            user.Id = id;

            // Отправка PUT-запроса на API
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7027/api/ApplicationUser/{id}", user);

            // Проверка успешности запроса
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update user with ID {id}. Status Code: {response.StatusCode}");
            }
        }


        //byid
        public async Task<ApplicationUser> GetUserById(string id)
        {
            // Отправка запроса на API
            var response = await _httpClient.GetAsync($"https://localhost:7027/api/ApplicationUser/{id}");

            // Проверка успешности запроса
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get user with ID {id}. Status Code: {response.StatusCode}");
            }

            // Десериализация ответа в объект ApplicationUser
            var user = await response.Content.ReadFromJsonAsync<ApplicationUser>();
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            return user;
        }


    }
}
