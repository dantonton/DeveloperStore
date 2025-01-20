namespace DeveloperStore.API.Models.Sales
{
    public class SalePutRequest
    {
        public Guid SaleId { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public ItemSalePostRequest[] Items { get; set; } = [];

    }
}
