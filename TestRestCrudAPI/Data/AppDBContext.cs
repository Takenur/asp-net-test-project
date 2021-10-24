using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Data
{
    public class AppDBContext : IdentityDbContext<Users>
    {
        public AppDBContext(DbContextOptions <AppDBContext> opt):base(opt)
        {

        }

        public DbSet <Items> Items { get; set; }
        public DbSet <Orders> Orders { get; set; }
        public DbSet <Regions> Regions { get; set; }
    }
}
