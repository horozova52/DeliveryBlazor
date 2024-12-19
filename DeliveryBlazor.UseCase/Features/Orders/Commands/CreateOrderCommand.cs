using MediatR;

namespace DeliveryBlazor.UseCase.Features.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }
        public Guid CourierId { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
    }
}
