using DeliveryBlazor.Core.Entities;
using MediatR;

namespace DeliveryBlazor.UseCase.Features.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public Guid Id { get; set; }
    }
}
