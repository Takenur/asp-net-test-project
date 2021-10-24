using System.ComponentModel.DataAnnotations;

namespace TestRestCrudAPI.Dtos
{
    public class ItemsCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string name { get; set; }
        [Required]
        public int cost { get; set; }
    }
}
