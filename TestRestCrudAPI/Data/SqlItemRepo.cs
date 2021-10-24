using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Data
{
    public class SqlItemRepo : IItemsRepo
    {
        private AppDBContext _context;

        public SqlItemRepo(AppDBContext context)
        {
            _context = context;
        }

        public void CreateItems(Items itm)
        {
            if (itm == null)
            {
                throw new ArgumentNullException(nameof(itm));
            }

            _context.Items.Add(itm);
        }

        public void DeleteItem(Items itm)
        {
            if (itm == null)
            {
                throw new ArgumentNullException(nameof(itm));
            }

            _context.Items.Remove(itm);

        }

        public Items GetItemById(int id)
        {
            return _context.Items.FirstOrDefault(p=>p.id==id);
        }

        public IEnumerable<Items> GetItems(PaginationFilter paginationFilter = null)
        {            
            if (paginationFilter == null)
            {
                return _context.Items.ToList();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return _context.Items.Skip(skip)
                .Take(paginationFilter.PageSize).ToList();
        }

        public int GetCount() {
            return _context.Items.Count();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0);
        }

        public void UpdateItems(Items itm)
        {
           
        }
    }
}
