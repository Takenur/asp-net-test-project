using System.Collections.Generic;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Data
{
    public interface IRegionsRepo
    {
        bool SaveChanges();
        IEnumerable<Regions> GetRegions(PaginationFilter paginationFilter = null);
        Regions GetRegionById(int id);
        IEnumerable<Regions> GetRegionsByParentId(int id, PaginationFilter paginationFilter = null);
        void CreateRegions(Regions regions);
        void UpdateRegions(Regions regions);
        void DeleteRegion(Regions regions);
        int GetCount();
    }
}
