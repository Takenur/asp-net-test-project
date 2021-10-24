using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Data;
using TestRestCrudAPI.Services;
using TestRestCrudAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace TestRestCrudAPI.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer
           (configuration.GetConnectionString("BackApiConnection")));
            services.AddIdentity<Users, IdentityRole>()
            .AddEntityFrameworkStores<AppDBContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IItemsRepo, SqlItemRepo>();
            services.AddScoped<IOrdersRepo, SqlOrdersRepo>();
            services.AddScoped<IRegionsRepo, SqlRegionsRepo>();
            
        }
    }
}
