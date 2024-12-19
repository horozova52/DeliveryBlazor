using MediatR;

namespace DeliveryApp.UseCase.Features.Client.Commands
{
    public class CreateClientCommand : IRequest<Guid>
    {
        public string UserId { get; set; }
    }
}
