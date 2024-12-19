using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace DeliveryBlazor.Client.Services.UserClientService
{
    public interface IUserServices
    {
        Task<Guid> CreateUserAndClientAsync(ApplicationUser user);
        Task<List<ApplicationUser>> GetAllApplicationUsersAsync();
        Task<Guid> DeleteUser(string id);
        Task<ApplicationUser> GetUserById(string id);
        Task UpdateUserAsync(string id, ApplicationUser user);

    }
}
