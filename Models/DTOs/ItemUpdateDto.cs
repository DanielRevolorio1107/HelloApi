using HelloApi.Models.DTOs;
namespace HelloApi.Models.DTOs
{
    public class ItemUpdateDto
    {
        public required string Name { get; set; }
        public required  decimal Price { get; set; }
    }
}
