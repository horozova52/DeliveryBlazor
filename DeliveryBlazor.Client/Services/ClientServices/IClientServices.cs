using DeliveryBlazor.Shared.DataTransferObjects;

namespace DeliveryBlazor.Client.Services.ClientService
{
    public interface IClientServices
    {
        Task<List<ApplicationUserDto>> GetAllClientsAsync();
    }
}
