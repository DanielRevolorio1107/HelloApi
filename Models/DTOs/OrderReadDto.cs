using HelloApi.Models.DTOs;

namespace HelloApi.Models.DTOs
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public PersonReadDto? Person { get; set; }
        public int Number { get; set; }

        // Mantengo "orderDetail" en singular porque así lo consume ahora tu front
        public List<OrderDetailReadDto> OrderDetail { get; set; } = new();
    }
}
