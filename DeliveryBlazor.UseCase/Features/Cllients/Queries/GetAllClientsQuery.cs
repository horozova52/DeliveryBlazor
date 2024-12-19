using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DeliveryBlazor.UseCase.Features.Clients.Queries
{
    // Query pentru a obține lista de utilizatori
    public class GetAllClientsQuery : IRequest<List<ApplicationUserDto>>
    {
        public GetAllClientsQuery() { }
    }

    // Handler pentru Query
    public class GetNotPaginatedClientsQueryHandler
        : IRequestHandler<GetAllClientsQuery, List<ApplicationUserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetNotPaginatedClientsQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUserDto>> Handle(
             GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            // Filtrare utilizatori cu rolul de Curier
            var users = _userManager.Users
                .Where(user => user.Role == DeliveryBlazor.Shared.Enums.UserRole.User);

            // Mapare în DTO
            var couriers = users.Select(user => new ApplicationUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Role = user.Role
            }).ToList();

            return await Task.FromResult(couriers);
        }
    }
}
