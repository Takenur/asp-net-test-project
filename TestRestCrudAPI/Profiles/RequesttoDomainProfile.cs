using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Requests.Queries;

namespace TestRestCrudAPI.Profiles
{
    public class RequesttoDomainProfile:Profile
    {
        public RequesttoDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
