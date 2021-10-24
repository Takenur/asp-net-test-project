using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Data
{
    public class SqlOrdersRepo : IOrdersRepo
    {
        private AppDBContext _context;
        public SqlOrdersRepo(AppDBContext context)
        {
            _context = context;
        }

        public void CreateOrders(Orders order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _context.Orders.Add(order);
        }

        public void DeleteOrder(Orders order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            _context.Orders.Remove(order);
        }

        public int GetCount()
        {
            return _context.Orders.Count();
        }

        public int GetCountByItems(string item)
        {
            return _context.Orders
                .Include(first => first.item)
                .Where(p => p.item.name.Contains(item)).Count();
        }

        public int GetCountByRegion(string region)
        {
            return _context.Orders
                 .Include(first => first.regions)
                 .Where(p => p.regions.name.Contains(region)).Count();
        }

        public Orders GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(p => p.id == id);
        }

        public IEnumerable<Orders> getOrders(PaginationFilter paginationFilter = null)
        {
            if (paginationFilter == null) {
                return _context.Orders.Include(first => first.item)
                    .Include(s => s.users)
                    .Include(t => t.regions).ToList();
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            return _context.Orders.Include(first => first.item)
                .Include(s=>s.users)
                .Include(t => t.regions)
                .Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToList();
        }

        public IEnumerable<Orders> getOrdersByItems(string item, PaginationFilter paginationFilter = null)
        {

            if (paginationFilter == null)
            {
                return _context.Orders
                .Include(first => first.item)
                .Where(p => p.item.name.Contains(item))
                .ToList();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return _context.Orders
                .Include(first => first.item)
                .Where(p => p.item.name.Contains(item))
                .Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToList();

        }

        public IEnumerable<Orders> getOrdersByRegion(string region, PaginationFilter paginationFilter = null)
        {
            
            if (paginationFilter == null)
            {
                return _context.Orders
                .Include(first => first.regions)
                .Where(p => p.regions.name.Contains(region))
                .ToList();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return _context.Orders
                .Include(first => first.regions)
                .Where(p => p.regions.name.Contains(region))
                .Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateOrders(Orders order)
        {
           
        }
    }
}
