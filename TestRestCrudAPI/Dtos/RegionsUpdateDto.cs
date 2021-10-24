using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Dtos
{
    public class RegionsUpdateDto
    {
        public int regionsId { get; set; }
        public virtual Regions regions { get; set; }
        [Required]
        [MaxLength(255)]
        public string name { get; set; }

        public List<Orders> users { get; set; }
    }
}
