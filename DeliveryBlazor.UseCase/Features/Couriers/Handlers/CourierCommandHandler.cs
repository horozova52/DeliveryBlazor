using DeliveryAppBlazor.Infrastructure.Contexts;
using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.UseCase.Features.Couriers.Commands;
using MediatR;

namespace DeliveryBlazor.UseCase.Features.Couriers.Handlers
{
    public class CourierCommandHandlers :
     IRequestHandler<CreateCourierCommand, Guid>,
     IRequestHandler<UpdateCourierCommand, Unit>,
     IRequestHandler<DeleteCourierCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public CourierCommandHandlers(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = new Courier { Id = Guid.NewGuid(), UserId = request.UserId };
            _context.Couriers.Add(courier);
            await _context.SaveChangesAsync(cancellationToken);
            return courier.Id;
        }

        public async Task<Unit> Handle(UpdateCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = await _context.Couriers.FindAsync(request.Id);
            if (courier == null) throw new KeyNotFoundException("Courier not found");

            courier.UserId = request.UserId;
            _context.Couriers.Update(courier);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = await _context.Couriers.FindAsync(request.Id);
            if (courier == null) throw new KeyNotFoundException("Courier not found");

            _context.Couriers.Remove(courier);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
