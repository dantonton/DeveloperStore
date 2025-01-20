namespace DeveloperStore.API.Models.Sales
{
    public class ItemSalePostRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
