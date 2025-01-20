using DeveloperStore.Domain.Commands;
using DeveloperStore.Domain.Common;
using DeveloperStore.Domain.Events;
using DeveloperStore.Domain.Infra;
using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Handlers
{
    public class SalesCancelCommandHandler : IRequestHandler<SalesCancelCommand, BaseResponse<Sale>>
    {
        private readonly IBaseRepository<Sale> _context;
        private readonly IMediator _mediator;

        public SalesCancelCommandHandler(IBaseRepository<Sale> context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<BaseResponse<Sale>> Handle(SalesCancelCommand request, CancellationToken cancellationToken)
        {
            var sale = await _context.GetByIdAsync(request.SaleId, cancellationToken);

            if (sale == null)
            {
                return BaseResponse<Sale>.NewErro("ResourceNotFound", "Sale not found", $"The sale with ID {request.SaleId} does not exist in our database.");
            }
            sale.IsCancelled = true;

            await _context.UpdateAsync(sale, cancellationToken);

            await _mediator.Publish(new SaleCancelledEvent(sale), cancellationToken);

            return BaseResponse<Sale>.New(sale);
        }
    }
}
