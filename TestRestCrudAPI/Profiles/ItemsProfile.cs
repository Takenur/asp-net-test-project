using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Dtos;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Profiles
{
    public class ItemsProfile :Profile
    {
        public ItemsProfile()
        {
            // Sourse->Target
            CreateMap<Items, ItemReadDto >();
            CreateMap<ItemsCreateDto, Items>();
            CreateMap<ItemUpdateDto, Items>();
            CreateMap<Items, ItemUpdateDto>();
        }
    }
}
