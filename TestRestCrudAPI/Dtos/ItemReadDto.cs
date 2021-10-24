using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestCrudAPI.Dtos
{
    public class ItemReadDto
    {
       
        public int id { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
    }
}
