using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Models;
using TestRestCrudAPI.Options;

namespace TestRestCrudAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Users> _userManager;
        private readonly JwtSettings _jwtSettings;

        public UserService(UserManager<Users> userManager, JwtSettings jwtSettings) {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email not exist" }
                };

            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user,password);
            if (!userHasValidPassword) {
                return new AuthenticationResult
                {
                    Errors = new[] { "User or password is wrong" }
                };
            }

            return GenerateAuthenticationResultForUser(user);

        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password,string name)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email already exist" }
                };
                
            }
            var newUser = new Users {
            Email=email,
            UserName=email,
            name= name
            };
            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };

            }

            return GenerateAuthenticationResultForUser(newUser);


        }

        private AuthenticationResult GenerateAuthenticationResultForUser(Users user) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
            new Claim(JwtRegisteredClaimNames.Sub,user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim("id",user.Id),

            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

    }
}
