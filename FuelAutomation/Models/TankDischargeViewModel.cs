namespace FuelAutomation.Models
{
    public class TankDischargeViewModel
    {
        public int? Id { get; set; }
        public string? FuelType { get; set; }
        public double? Capacity { get; set; }
        public double? Quantity { get; set; }
        public string? DischargeQuantity { get; set; }
    }
}
