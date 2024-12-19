using DeliveryBlazor.Core.Entities;
using MediatR;

namespace DeliveryBlazor.UseCase.Features.Couriers.Queries
{
    public class GetCourierByIdQuery : IRequest<Courier>
    {
        public Guid Id { get; set; }
    }
}
