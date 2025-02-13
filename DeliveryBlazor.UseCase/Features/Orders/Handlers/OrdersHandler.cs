using DeliveryAppBlazor.Infrastructure.Contexts;
using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.UseCase.Features.Orders.Commands;
using DeliveryBlazor.UseCase.Features.Orders.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryBlazor.UseCase.Features.Orders.Handlers
{
    public class OrderHandlers :
        IRequestHandler<CreateOrderCommand, Guid>,
        IRequestHandler<UpdateOrderCommand, Unit>,
        IRequestHandler<DeleteOrderCommand, Unit>,
       // IRequestHandler<GetAllOrdersQuery, List<Order>>,
        IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly ApplicationDbContext _context;

        public OrderHandlers(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ClientId = request.ClientId,
                CourierId = request.CourierId,
                Status = request.Status,
                Address = request.Address,
                DataCreated = DateTime.UtcNow,
                DataUpdated = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            return order.Id;
        }

        // Update
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.Id);
            if (order == null) throw new Exception("Order not found");

            order.Status = request.Status;
            order.Address = request.Address;
            order.DataUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value; // Unit este returnat pentru a respecta semnătura interfeței
        }


        // Delete
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.Id);
            if (order == null) throw new KeyNotFoundException("Order not found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        // Get All
        public async Task<List<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders.ToListAsync(cancellationToken);
        }

        // Get By Id
        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.Id);
            if (order == null) throw new KeyNotFoundException("Order not found");

            return order;
        }
    }
}