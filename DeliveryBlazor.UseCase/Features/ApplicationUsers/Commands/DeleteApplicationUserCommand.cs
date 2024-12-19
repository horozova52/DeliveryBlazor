
using MediatR;

namespace DeliveryBlazor.UseCase.Features.ApplicationUsers.Commands
{
    public class DeleteApplicationUserCommand : IRequest<Unit>
    {
        public string Id { get; set; }

    }


}
