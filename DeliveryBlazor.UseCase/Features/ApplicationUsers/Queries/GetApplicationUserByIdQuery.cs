using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DeliveryBlazor.UseCase.Features.ApplicationUsers.Queries
{
    public class GetApplicationUserByIdQuery : IRequest<ApplicationUserDto>
    {
        public string Id { get; set; }
    }
    // Handler dedicat pentru "Get By Id"
    public class GetApplicationUserByIdQueryHandler
        : IRequestHandler<GetApplicationUserByIdQuery, ApplicationUserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetApplicationUserByIdQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUserDto> Handle(
            GetApplicationUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            // Caută userul
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new Exception($"User with ID {request.Id} not found.");
            }

            // Mapează la DTO
            return new ApplicationUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
    }
