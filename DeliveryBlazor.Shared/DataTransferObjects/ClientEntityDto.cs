
using DeliveryBlazor.Shared.Enums;
namespace DeliveryBlazor.Shared.DataTransferObjects
{
    public class ClientEntityDto
    {
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public UserRole Role { get; set; }

        
    }
}
