using DeliveryBlazor.Shared.DataTransferObjects;

namespace DeliveryBlazor.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<ApplicationUserDto>> GetAllOrdersAsync();
    }
}
