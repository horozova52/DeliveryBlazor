using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryBlazor.Shared.DataTransferObjects
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid CourierId { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public DateTime DataCreated { get; set; } = DateTime.Now;
        public DateTime DataUpdated { get; set; }
    }
}
