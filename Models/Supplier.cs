namespace AppStockControl.Models
{
    public class Supplier
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? TrandName { get; set; }
        public string Cnpj { get; set; }
        public string? StateRegistration { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? ContactName { get; set; }
        public string? Observation { get; set; }
        public Address Address { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}