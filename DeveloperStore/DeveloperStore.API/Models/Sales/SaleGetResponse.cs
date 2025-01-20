namespace DeveloperStore.API.Models.Sales
{
    public class SaleGetResponse
    {
        public Guid SaleId { get; set; } = Guid.NewGuid();
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public ItemSaleGetResponse[] Items { get; set; } = [];
    }
}
