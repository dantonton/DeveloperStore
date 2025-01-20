namespace DeveloperStore.Domain.Models
{
    public class Sale : BaseModel
    {
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
        public List<SaleItem> Items { get; set; } = new();
        public decimal Total => Items.Sum(item => item.Total);
        public bool IsCancelled { get; set; } = false;

        public string? Validate()
        {
            var invalidItem = Items.Where(x => x.Quantity > 20);
            if (invalidItem.Any())
            {
                var products = string.Join(", ", invalidItem.Select(x=>x.ProductName));
                return $"The products ({products}) must contain 20 or less items";
            }
            return null;
        }

        internal SaleItemCancelled[] SetItems(List<SaleItem> newItems)
        {
            var cancelleds = new List<SaleItemCancelled>();
            foreach (var oldItem in Items)
            {
                var newItem = newItems.FirstOrDefault(x => x.ProductName == oldItem.ProductName);
                if (newItem == null)
                {
                    cancelleds.Add(new SaleItemCancelled()
                    {
                        ProductName = oldItem.ProductName,
                        Quantity = oldItem.Quantity,
                    });
                }else if(newItem.Quantity < oldItem.Quantity) {

                    cancelleds.Add(new SaleItemCancelled()
                    {
                        ProductName = oldItem.ProductName,
                        Quantity = oldItem.Quantity - newItem.Quantity,
                    });
                }
            }
            Items = newItems;
            return [.. cancelleds];
        }
    }
}
