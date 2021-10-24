
using System.Collections.Generic;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Dtos
{
    public class RegionsReadDto
    {
        
        public int Id { get; set; }
        public int regionsId { get; set; }
        public virtual Regions regions { get; set; }
        public string name { get; set; }
        public List<Orders> users { get; set; }
    }
}
