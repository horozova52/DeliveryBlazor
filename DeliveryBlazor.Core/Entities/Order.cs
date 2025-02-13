using DeliveryBlazor.Shared.Enums;

namespace DeliveryBlazor.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public ClientEntity Client { get; set; }
        public Guid ClientId { get; set; }

        public Guid CourierId { get; set; }
        public Courier Courier { get; set; }

        public string Status { get; set; }
        public string Address { get; set; }
        public DateTime DataCreated { get; set; } = DateTime.Now;
        public DateTime DataUpdated { get; set; }

    }
}
