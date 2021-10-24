using System.ComponentModel.DataAnnotations;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Dtos
{
    public class OrdersCreateDto
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
