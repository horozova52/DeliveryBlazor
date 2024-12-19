namespace DeliveryBlazor.Core.Entities
{
    public class Courier
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


    }
}
