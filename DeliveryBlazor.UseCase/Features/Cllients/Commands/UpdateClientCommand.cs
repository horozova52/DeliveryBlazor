using MediatR;

namespace DeliveryApp.UseCase.Features.Client.Commands
{
    public class UpdateClientCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}
