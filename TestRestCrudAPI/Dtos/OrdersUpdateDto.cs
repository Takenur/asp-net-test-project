using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Dtos
{
    public class OrdersUpdateDto
    {
        [Required]
        public int date { get; set; }
        [Required]
        public int regionsId { get; set; }
        public virtual Regions regions { get; set; }

        [Required]
        public string products { get; set; }
        [Required]
        public int sum { get; set; }
    }
}
