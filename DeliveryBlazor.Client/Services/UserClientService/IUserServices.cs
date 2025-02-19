using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;

namespace DeliveryBlazor.Client.Services.UserClientService
{
    public interface IUserServices
    {
        Task<Guid> CreateUserAndClientAsync(ApplicationUserDto user, string? password = null);
        Task<List<ApplicationUserDto>> GetAllApplicationUsersAsync();
        Task<Guid> DeleteUser(string id);
        Task<ApplicationUserDto> GetUserById(string id);
        Task UpdateUserAsync(string id, ApplicationUserDto user);

    }
}
