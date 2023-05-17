namespace FuelAutomation.Models
{
    public class SaleHistoryViewModel
    {
        public DateTimeOffset CreatedOn { get; set; } //tarih
        public string? CarPlate { get; set; } //plaka

        public string? Quantity { get; set; }
        public string? Price { get; set; }
        public string? Name { get; set; }
        public string? FuelType { get; set; }
    }
}
