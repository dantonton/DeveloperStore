namespace DeveloperStore.API.Models.Sales
{
    public class SalePostRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public ItemSalePostRequest[] Items { get; set; } = [];

    }
}
