using HelloApi.Models;


namespace HelloApi.Models.DTOs
{
    public class ItemreadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
