using HelloApi.Models.DTOs;

namespace HelloApi.Models.DTOs
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public PersonReadDto? Person { get; set; }
        public int Number {  get; set; }
        public List<Models.OrderDetail>OrderDetail { get; set; }
    }
}
