using DeliveryBlazor.Shared.Enums;
using System.Security.Principal;

namespace DeliveryBlazor.Shared.DataTransferObjects
{
    public class ApplicationUserDto 
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
