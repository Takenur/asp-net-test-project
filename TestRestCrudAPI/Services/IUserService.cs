using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Domain;

namespace TestRestCrudAPI.Services
{
    public interface IUserService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password,string name);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}
