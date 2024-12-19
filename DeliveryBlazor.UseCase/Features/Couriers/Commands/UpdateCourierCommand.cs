using MediatR;

namespace DeliveryBlazor.UseCase.Features.Couriers.Commands
{
    public class UpdateCourierCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}
