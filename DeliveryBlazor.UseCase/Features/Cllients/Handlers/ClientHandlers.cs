using DeliveryApp.UseCase.Features.Client.Commands;
using DeliveryAppBlazor.Infrastructure.Contexts;
using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.UseCase.Features.Clients.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryBlazor.UseCase.Features.Clients.Handlers
{
    public class ClientHandlers :
        IRequestHandler<CreateClientCommand, Guid>,
        IRequestHandler<UpdateClientCommand, Unit>,
        IRequestHandler<DeleteClientCommand, Unit>,
       //RequestHandler<GetAllClientsQuery, List<ClientEntity>>,
        IRequestHandler<GetClientByIdQuery, ClientEntity>
    {
        private readonly ApplicationDbContext _context;

        public ClientHandlers(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Create
        public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var client = new ClientEntity
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId
            };

            await _context.Clients.AddAsync(client, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return client.Id;
        }

        // Update
        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var client = await _context.Clients.FindAsync(request.Id);
            if (client == null) throw new KeyNotFoundException("Client not found");

            client.UserId = request.UserId;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        // Delete
        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var client = await _context.Clients.FindAsync(request.Id);
            if (client == null)
            {
                // Log the error or handle it as needed
                return Unit.Value; // Or return a specific result indicating failure
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }


        // Get All
        public async Task<List<ClientEntity>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clients.AsNoTracking().ToListAsync(cancellationToken);
        }

        // Get By Id
        public async Task<ClientEntity> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var client = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (client == null) throw new KeyNotFoundException("Client not found");

            return client;
        }
    }
}
