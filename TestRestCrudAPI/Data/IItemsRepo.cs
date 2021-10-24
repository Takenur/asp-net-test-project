using System.Collections.Generic;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Data
{
    public interface IItemsRepo
    {
        bool SaveChanges();
        IEnumerable<Items> GetItems(PaginationFilter paginationFilter = null);
        Items GetItemById(int id);
        void CreateItems(Items itm);
        void UpdateItems(Items itm);
        void DeleteItem(Items itm);

        int GetCount();
    }
}
