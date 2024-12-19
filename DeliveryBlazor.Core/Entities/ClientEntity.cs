using DeliveryBlazor.Core.Entities;

namespace DeliveryBlazor.Core.Entities
{
    public class ClientEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Order> Orders { get; set; }
        //public UserRole Role { get; set; }

    }
}
