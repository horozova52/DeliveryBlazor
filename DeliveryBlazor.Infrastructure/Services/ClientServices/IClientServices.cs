using DeliveryBlazor.Shared.DataTransferObjects;

namespace DeliveryBlazor.Infrastructure.Services.ClientService
{
    public interface IClientServices
    {
        Task<List<ApplicationUserDto>> GetAllClientsAsync();
    }
}
