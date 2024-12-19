using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DeliveryBlazor.UseCase.Features.Orders.Queries
{
    // Query pentru a obține lista de utilizatori
    public class GetAllOrdersQuery : IRequest<List<ApplicationUserDto>>
    {
        public GetAllOrdersQuery() { }
    }

    // Handler pentru Query
    public class GetNotPaginatedOrdersQueryHandler
        : IRequestHandler<GetAllOrdersQuery, List<ApplicationUserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetNotPaginatedOrdersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUserDto>> Handle(
            GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            // Obține utilizatorii din baza de date și mapează-i în DTO
            var users = _userManager.Users;

            return await Task.FromResult(users.Select(user => new ApplicationUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,

            }).ToList());
        }
    }
}