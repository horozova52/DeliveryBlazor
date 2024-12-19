using MediatR;

namespace DeliveryBlazor.UseCase.Features.Couriers.Commands
{
    public class DeleteCourierCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
