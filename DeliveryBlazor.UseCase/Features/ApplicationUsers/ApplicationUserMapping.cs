using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Shared.DataTransferObjects;
using System.Linq.Expressions;
namespace DeliveryBlazor.UseCase.Features.ApplicationUsers
{
    public class ApplicationUserMapping
    {
        public static Expression<Func<ApplicationUser, ApplicationUserDto>> ToApplicationUserDto
        {
            get
            {
                return user => new ApplicationUserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    Role = user.Role
                };
            }
        }
    }
}
