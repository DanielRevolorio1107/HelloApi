namespace HelloApi.Models.DTOs
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public MiniOrderDto? Order { get; set; }
        public int ItemId { get; set; }
        public MiniItemDto? Item { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
