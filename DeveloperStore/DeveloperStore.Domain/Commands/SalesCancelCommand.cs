using DeveloperStore.Domain.Common;
using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Commands
{
    public class SalesCancelCommand : IRequest<BaseResponse<Sale>>
    {
        public Guid SaleId { get; set; }
    }
}
