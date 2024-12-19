using DeliveryBlazor.Core.Entities;
using MediatR;

namespace DeliveryBlazor.UseCase.Features.ApplicationUsers.Queries
{
    public class GetApplicationUserByIdQuery : IRequest<ApplicationUser>
    {
        public string Id { get; set; }
    }
}
