using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Events
{
    public class SaleModifiedEvent(Sale sale, decimal oldTotal) : INotification
    {
        public Sale Sale { get; } = sale;
        public decimal OldTotal { get; } = oldTotal;
    }
}
