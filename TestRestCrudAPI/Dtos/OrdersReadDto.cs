using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Dtos
{
    public class OrdersReadDto
    {
        public int id { get; set; }
        public int date { get; set; }
        public int regionsId { get; set; }
        public virtual Regions regions { get; set; }
        public string products { get; set; }
        public int sum { get; set; }
    }
}
