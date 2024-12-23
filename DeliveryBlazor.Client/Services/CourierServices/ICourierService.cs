using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace DeliveryBlazor.Client.Services.CourierService
{
    public interface ICourierService
    {
        Task<List<ApplicationUser>> GetAllCouriersAsync();
        Task DeleteCourierAsync(string id);
    }
}
