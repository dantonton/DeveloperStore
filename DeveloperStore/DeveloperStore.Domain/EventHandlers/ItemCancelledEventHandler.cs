using DeveloperStore.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperStore.Domain.EventHandlers
{
    public class ItemCancelledEventHandler(ILogger<SaleCreatedEventHandler> logger) : INotificationHandler<ItemCancelledEvent>
    {
        private readonly ILogger<SaleCreatedEventHandler> _logger = logger;

        public async Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            var sale = notification.Sale;
            var products = string.Join(", ", notification.Cancelled.Select(x => $"{x.Quantity} {x.ProductName}"));
            string message = $"Itens cancelled with sale ID: {sale.Id}, Products: {products}";
            _logger.LogInformation(message);

        }
    }
}
