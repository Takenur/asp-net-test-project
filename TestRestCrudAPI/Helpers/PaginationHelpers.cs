using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Response;

namespace TestRestCrudAPI.Helpers
{
    public class PaginationHelpers
    {
        public static object CreatePaginationResponse<T> (List<T> Response,PaginationFilter pagination,int count) {
            var paginationResponse = new PagedResponse<T>
            {
                Data = Response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                TotalCount = Response.Count()
            };


            return paginationResponse;


        }
    }
}
