using AutoMapper;
using TestRestCrudAPI.Dtos;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            // Sourse->Target
            CreateMap<Orders, OrdersReadDto>();
            CreateMap<OrdersCreateDto, Orders>();
            CreateMap<OrdersUpdateDto, Orders>();
            CreateMap<Orders, OrdersUpdateDto>();
        }
    }
}
