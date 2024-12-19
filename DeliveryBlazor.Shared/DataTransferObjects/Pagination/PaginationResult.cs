using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryBlazor.Shared.DataTransferObjects.Pagination;

public class PaginationResult<T>
{
    public int TotalItems { get; set; }
    public IEnumerable<T> Items { get; set; }
}
