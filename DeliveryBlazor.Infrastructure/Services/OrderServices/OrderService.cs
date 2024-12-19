using DeliveryBlazor.Shared.DataTransferObjects;
using System.Net.Http.Json;

namespace DeliveryBlazor.Infrastructure.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<ApplicationUserDto>> GetAllOrdersAsync()
        {
            // Utilizează HttpClient pentru a apela endpoint-ul GetAllUsers
            var response = await _httpClient.GetAsync("api/Orders");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch users. Status Code: {response.StatusCode}");
            }

            var users = await response.Content.ReadFromJsonAsync<List<ApplicationUserDto>>();
            return users ?? new List<ApplicationUserDto>();
        }
    }
}
