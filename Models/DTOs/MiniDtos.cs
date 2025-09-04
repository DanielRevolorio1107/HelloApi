namespace HelloApi.Models.DTOs
{
    public class MiniOrderDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
    }

    public class MiniItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
