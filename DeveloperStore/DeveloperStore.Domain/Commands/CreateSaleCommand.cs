using DeveloperStore.Domain.Common;
using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Commands
{
    public class CreateSaleCommand : IRequest<BaseResponse<Sale>>
    {
        public string Customer { get; set; }
        public string Branch { get; set; }
        public List<SaleItem> Items { get; set; } = [];
    }
}
