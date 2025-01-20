using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Events
{
    public class ItemCancelledEvent(Sale sale, SaleItemCancelled[] cancelleds) : INotification
    {
        public Sale Sale { get; } = sale;
        public SaleItemCancelled[] Cancelled { get; } = cancelleds;
    }
}
