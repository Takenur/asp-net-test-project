using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestCrudAPI.Dtos
{
    public class ItemUpdateDto
    {
        [Required]
        [MaxLength(255)]
        public string name { get; set; }
        [Required]
        public int cost { get; set; }
    }
}
