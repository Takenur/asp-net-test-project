using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestCrudAPI.Models
{
    public class Orders
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int date { get; set; }
        [Required]
        [ForeignKey("Regions")]
        public int regionsId { get; set; }
        [Required]
        [ForeignKey("Items")]
        public string product_id { get; set; }
        [Required]
        public int count { get; set; }
        [Required]
        public int sum { get; set; }

        public Items item { get; set; }
        public Regions regions { get; set; }

        public Users users { get; set; }


    }
}
