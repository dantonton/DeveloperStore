namespace DeveloperStore.API.Models.Sales
{
    public class ItemSaleGetResponse
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantities { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}
