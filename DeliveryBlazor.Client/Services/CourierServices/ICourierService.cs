using DeliveryBlazor.Shared.DataTransferObjects;

namespace DeliveryBlazor.Client.Services.CourierService
{
    public interface ICourierService
    {
        Task<List<ApplicationUserDto>> GetAllCouriersAsync();
    }
}
