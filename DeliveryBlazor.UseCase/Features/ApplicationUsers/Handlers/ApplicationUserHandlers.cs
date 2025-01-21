using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.UseCase.Resources;
using DeliveryBlazor.UseCase.Features.ApplicationUsers.Commands;
using DeliveryBlazor.UseCase.Features.ApplicationUsers.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeliveryBlazor.UseCase.Features.ApplicationUsers.Handlers
{
    public class ApplicationUserHandler :
        IRequestHandler<GetApplicationUserByIdQuery, ApplicationUser>,
        IRequestHandler<CreateApplicationUserCommand, string>,
        IRequestHandler<UpdateApplicationUserCommand, Unit>,
        IRequestHandler<DeleteApplicationUserCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Metodă auxiliară pentru găsirea utilizatorului sau aruncarea excepției
        private async Task<ApplicationUser> FindUserOrThrow(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new Exception(ErrorMessages.UserNotFound);
            return user;
        }

        // Metodă auxiliară pentru validarea rezultatelor
        private void CheckResult(IdentityResult result, string errorMessage)
        {
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                var detailedErrorMessage = $"{errorMessage}: {errors}";
                throw new Exception(detailedErrorMessage);
            }
        }

        // Get User By Id
        public async Task<ApplicationUser> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await FindUserOrThrow(request.Id);

            // Dacă sunt necesare câmpuri suplimentare, poți extinde logica aici
            return new ApplicationUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                //Address = user.Address, // Asigură-te că Address este populat
                //Role = user.Role // Asigură-te că Role este populat
            };
          //  return await FindUserOrThrow(request.Id);
        }

        // Create User
        public async Task<string> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
               // UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
               // Address = request.Address,
               // Role = request.Role
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            CheckResult(result, ErrorMessages.CreateUserFailed);

            return user.Id;
        }

        // Update User
        public async Task<Unit> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var user = await FindUserOrThrow(request.Id);

            // Actualizează doar FirstName și LastName
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;

            var result = await _userManager.UpdateAsync(user);
            CheckResult(result, ErrorMessages.UpdateUserFailed);

            return Unit.Value;
        }



        // Delete User
        public async Task<Unit> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var user = await FindUserOrThrow(request.Id);

            var result = await _userManager.DeleteAsync(user);
            CheckResult(result, ErrorMessages.DeleteUserFailed);

            return Unit.Value;
        }
    }
}
