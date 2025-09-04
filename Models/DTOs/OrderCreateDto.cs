namespace HelloApi.Models.DTOs
{
    public class OrderCreateDto
    {
        public required int PersonId { get; set; }
        public int Number { get; set; }
    }
}
