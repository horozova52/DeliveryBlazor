using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace DeliveryBlazor.Infrastructure.Services.UserClientService
{
    public interface IUserServices
    {
        Task<List<ApplicationUserDto>> GetAllApplicationUsersAsync();
        Task<Guid> DeleteUser(string id);

    }
}
