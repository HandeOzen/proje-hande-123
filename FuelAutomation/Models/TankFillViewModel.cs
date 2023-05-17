namespace FuelAutomation.Models
{
    public class TankFillViewModel
    {
        public int? Id { get; set; }
        public string? FuelType { get; set; }
        public double? Capacity { get; set; }
        public double? Quantity { get; set; }
        public string? FillQuantity { get; set; }
    }
}
