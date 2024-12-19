using MediatR;

namespace DeliveryBlazor.UseCase.Features.Orders.Commands
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
