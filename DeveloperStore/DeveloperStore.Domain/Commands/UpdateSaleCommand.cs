using DeveloperStore.Domain.Common;
using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Commands
{
    public class UpdateSaleCommand : IRequest<BaseResponse<Sale>>
    {
        public Guid SaleId { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
        public List<SaleItem> Items { get; set; } = [];
    }
}
