using MediatR;

namespace DeliveryBlazor.UseCase.Features.Couriers.Commands
{
    public class CreateCourierCommand : IRequest<Guid>
    {
        public string UserId { get; set; }
    }
}
