namespace DeveloperStore.Domain.Models
{
    public class SaleItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount => GetDiscount();
        public decimal Total => Quantity * UnitPrice - Discount;

        private decimal GetDiscount()
        {
            if (Quantity >= 10 && Quantity <= 20) return Quantity * UnitPrice * 0.2m;
            if (Quantity >= 4) return Quantity * UnitPrice * 0.1m;
            return 0;
        }
    }
}
