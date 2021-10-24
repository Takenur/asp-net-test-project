using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestRestCrudAPI.Models
{
    public class Regions
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Regions")]
        public int? RegionsId { get; set; }
        [Required]
        [MaxLength(255)]
        public string name { get; set; }
        public List<Regions> Region { get; set; }
    }
}
