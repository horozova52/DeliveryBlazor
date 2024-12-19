using DeliveryBlazor.Core.Entities;
using MediatR;

namespace DeliveryBlazor.UseCase.Features.Clients.Queries
{
    public class GetClientByIdQuery : IRequest<ClientEntity>
    {
        public Guid Id { get; set; }
    }
}
