using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Events
{
    public class SaleCreatedEvent(Sale sale) : INotification
    {
        public Sale Sale { get; } = sale;
    }
}
