using MediatR;

namespace DeliveryApp.UseCase.Features.Client.Commands
{
    public class DeleteClientCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
