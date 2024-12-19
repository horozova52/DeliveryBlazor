using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryBlazor.Shared.DataTransferObjects.Pagination
{
    public interface IPaginationService
    {
        Task<PaginationResult<DestinationT>> PaginateEnumerableAsync<TSource, DestinationT>(IEnumerable<TSource> source, PaginationParameter request);

        Task<PaginationResult<DestinationT>> PaginateAsync<TSource, DestinationT>(IQueryable<TSource> source, PaginationParameter request, Expression<Func<TSource, DestinationT>> MappingRule);
    }
}
