using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Data
{
    public class SqlRegionsRepo : IRegionsRepo
    {
        private AppDBContext _context;

        public SqlRegionsRepo(AppDBContext context)
        {
            _context = context;
        }
        public void CreateRegions(Regions regions)
        {
            if (regions == null)
            {
                throw new ArgumentNullException(nameof(regions));
            }
            _context.Regions.Add(regions);
        }

        public void DeleteRegion(Regions regions)
        {
            if (regions == null)
            {
                throw new ArgumentNullException(nameof(regions));
            }

            _context.Regions.Remove(regions);
        }

        public int GetCount()
        {
            return _context.Regions
                .Include(first => first.Region)
                .ThenInclude(second => second.Region)
                .Where(p => p.RegionsId == null)
                .Count();
        }

        public Regions GetRegionById(int id)
        {
            return _context.Regions.FirstOrDefault(p => p.id == id);
        }

        public IEnumerable<Regions> GetRegions(PaginationFilter paginationFilter = null)
        {
            if (paginationFilter == null)
            {
                return _context.Regions
                .Include(first => first.Region)
                .ThenInclude(second => second.Region)
                .Where(p => p.RegionsId == null)
                .ToList();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            return _context.Regions
                .Include(first => first.Region)
                .ThenInclude(second => second.Region)
                .Where(p => p.RegionsId == null)
                .Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToList();

        }

        public IEnumerable<Regions> GetRegionsByParentId(int id, PaginationFilter paginationFilter = null)
        {
            
            if (paginationFilter == null)
            {
                return _context.Regions.Where(p => p.RegionsId == id).ToList();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return _context.Regions.Where(p => p.RegionsId == id)
                .Skip(skip)
                .Take(paginationFilter.PageSize).ToList();

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateRegions(Regions regions)
        {
        }
    }
}
