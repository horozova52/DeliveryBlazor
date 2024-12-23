using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace DeliveryBlazor.Client.Services.ClientService
{
    public interface IClientServices
    {
        Task<List<ApplicationUser>> GetAllClientsAsync();
        Task DeleteClientAsync(string id);
    
}
}
