using DeliveryBlazor.Shared.DataTransferObjects;

namespace DeliveryBlazor.Infrastructure.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<ApplicationUserDto>> GetAllOrdersAsync();
    }
}
