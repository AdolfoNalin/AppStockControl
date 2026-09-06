namespace AppStockControl.Models
{
    public enum UnitType
    {
        Unit,
        Kg,
        Liter,
        Box,
        Package
    }

    public class Product
    {
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public Guid UserId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public int MinimumStock { get; set; }
        public int MaximumStock { get; set; }
        public decimal BuyPrice{ get; set; }
        public decimal SalePrice{ get; set; }
        public UnitType UnitType { get; set; }
        public bool IsActive { get; set; }
        public string? Barcode{ get; set; }
        public decimal ProfitMargin =>
        BuyPrice == 0
        ? 0
        : ((SalePrice - BuyPrice) / BuyPrice) * 100;
        public DateOnly CreatedAt { get; set; }
        public DateOnly? UpdatedAt { get; set; }
        public string? ImagePath { get; set; }
        public string? Observation { get; set; }
    }
}
