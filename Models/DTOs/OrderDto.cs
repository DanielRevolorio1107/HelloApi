namespace HelloApi.Models.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public PersonReadDto? Person { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Mantengo "OrderDetail" (singular) porque así lo consume tu front
        public List<OrderDetailReadDto> OrderDetail { get; set; } = new();
    }
}
