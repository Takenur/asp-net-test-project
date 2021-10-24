using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TestRestCrudAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings),jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddAuthentication(
                x => { 
                x.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(x=> {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer=false,
                        ValidateAudience=false,
                        RequireExpirationTime=false,
                        ValidateLifetime=true
                    };
                });
            services.AddControllers().AddNewtonsoftJson(
                s =>
                {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
                );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
