﻿using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeliveryBlazor.UseCase.Features.ApplicationUsers.Queries
{
    // Query pentru a obține lista de utilizatori
    public class GetAllApplicationUsersQuery : IRequest<List<ApplicationUser>>
    {
        public GetAllApplicationUsersQuery() { }
    }

    // Handler pentru Query
    public class GetAllApplicationUsersQueryHandler
        : IRequestHandler<GetAllApplicationUsersQuery, List<ApplicationUser>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllApplicationUsersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> Handle(GetAllApplicationUsersQuery request, CancellationToken cancellationToken)
        {
            // Obține utilizatorii din UserManager
            var users = await _userManager.Users.ToListAsync(cancellationToken);

            // Mapează utilizatorii în DTO-uri
            return users.Select(user => new ApplicationUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email= user.Email,
                Role = user.Role 
            }).ToList();
        }
    }
}
