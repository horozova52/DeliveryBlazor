using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using DeliveryBlazor.Shared.Enums;
using DeliveryBlazor.UseCase.Resources;
using DeliveryBlazor.UseCase.Features.ApplicationUsers.Commands;
using DeliveryBlazor.UseCase.Features.ApplicationUsers.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeliveryBlazor.UseCase.Features.ApplicationUsers.Handlers
{
    public class ApplicationUserHandler :
       // IRequestHandler<GetApplicationUserByIdQuery, ApplicationUserDto>,
        //IRequestHandler<GetAllApplicationUsersQuery, List<ApplicationUserDto>>,
        IRequestHandler<CreateApplicationUserCommand, string>,
        IRequestHandler<UpdateApplicationUserCommand, Unit>,
        IRequestHandler<DeleteApplicationUserCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Metodă auxiliară pentru validarea rezultatelor Identity
        private void CheckResult(IdentityResult result, string errorMessage)
        {
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                var detailedErrorMessage = $"{errorMessage}: {errors}";
                throw new Exception(detailedErrorMessage);
            }
        }

        // Metodă auxiliară pentru găsirea utilizatorului (aruncă excepție dacă nu există)
        private async Task<ApplicationUser> FindUserOrThrow(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception(ErrorMessages.UserNotFound);

            return user;
        }

      
        // create
        public async Task<string> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,   // Sau direct request.Email
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                Role = request.Role
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            CheckResult(result, ErrorMessages.CreateUserFailed);

            return user.Id; // Returnează Id-ul noului user
        }

        // update
        public async Task<Unit> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            // Caută user după ID
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
                throw new Exception($"User with Id={request.Id} not found.");

            // Actualizează câmpurile
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Address = request.Address;
            user.Role = request.Role;

            // Face update
            var result = await _userManager.UpdateAsync(user);
            CheckResult(result, "Failed to update user");

            return Unit.Value;
        }


        // delete
        public async Task<Unit> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
                throw new Exception($"User with Id={request.Id} not found.");

            var result = await _userManager.DeleteAsync(user);
            CheckResult(result, "Failed to delete user");

            return Unit.Value;
        }
    }
}
