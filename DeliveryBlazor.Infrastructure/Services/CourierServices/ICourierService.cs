using DeliveryBlazor.Shared.DataTransferObjects;

namespace DeliveryBlazor.Infrastructure.Services.CourierService
{
    public interface ICourierService
    {
        Task<List<ApplicationUserDto>> GetAllCouriersAsync();
    }
}
