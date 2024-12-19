using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DeliveryBlazor.UseCase.Features.Couriers.Queries
{
    // Query pentru a obține lista de utilizatori
    public class GetAllCouriersQuery : IRequest<List<ApplicationUserDto>>
    {
        public GetAllCouriersQuery() { }
    }

    // Handler pentru Query
    public class GetNotPaginatedCouriersQueryHandler
        : IRequestHandler<GetAllCouriersQuery, List<ApplicationUserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetNotPaginatedCouriersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUserDto>> Handle(
             GetAllCouriersQuery request, CancellationToken cancellationToken)
        {
            // Filtrare utilizatori cu rolul de Curier
            var users = _userManager.Users
                .Where(user => user.Role == DeliveryBlazor.Shared.Enums.UserRole.Courier);

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
