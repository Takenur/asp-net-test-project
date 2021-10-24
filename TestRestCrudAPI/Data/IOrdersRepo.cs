using System.Collections.Generic;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Data
{
    public interface IOrdersRepo
    {
        bool SaveChanges();
        IEnumerable<Orders> getOrders(PaginationFilter paginationFilter=null);
        Orders GetOrderById(int id);
        void CreateOrders(Orders order);
        void UpdateOrders(Orders order);
        void DeleteOrder(Orders order);

        IEnumerable<Orders> getOrdersByRegion(string region, PaginationFilter paginationFilter = null);

        IEnumerable<Orders> getOrdersByItems(string item, PaginationFilter paginationFilter = null);
        int GetCount();
        int GetCountByRegion(string region);
        int GetCountByItems(string items);

    }
}
