namespace AppStockControl.Models
{
    public enum MovimentType
    {
        Entry,
        Exit,
        Ajustment
    }

    public class StockMovement
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public MovimentType MovimentType { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalValue { get; }
        public string? Observation { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
