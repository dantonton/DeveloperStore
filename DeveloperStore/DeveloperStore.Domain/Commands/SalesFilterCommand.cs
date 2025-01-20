using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Commands
{
    public class SalesFilterCommand : IRequest<IEnumerable<Sale>>
    {
        public Guid? FilterBySaleId { get; set; }
    }
}
