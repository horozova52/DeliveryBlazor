using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeliveryBlazor.UseCase.Features.ApplicationUsers.Queries
{
    // Query pentru a obține lista de utilizatori
    public class GetAllApplicationUsersQuery : IRequest<List<ApplicationUserDto>>
    {
        public GetAllApplicationUsersQuery() { }
    }

    // Handler pentru Query
    public class GetAllApplicationUsersQueryHandler
        : IRequestHandler<GetAllApplicationUsersQuery, List<ApplicationUserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllApplicationUsersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUserDto>> Handle(GetAllApplicationUsersQuery request, CancellationToken cancellationToken)
        {
            // Obține utilizatorii din UserManager
            var users = await _userManager.Users.ToListAsync(cancellationToken);

            // Mapează utilizatorii în DTO-uri
            return users.Select(user => new ApplicationUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Role = user.Role 
            }).ToList();
        }
    }
}
