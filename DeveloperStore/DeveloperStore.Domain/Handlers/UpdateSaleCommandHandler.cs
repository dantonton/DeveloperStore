using DeveloperStore.Domain.Commands;
using DeveloperStore.Domain.Common;
using DeveloperStore.Domain.Events;
using DeveloperStore.Domain.Infra;
using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Handlers
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, BaseResponse<Sale>>
    {
        private readonly IBaseRepository<Sale> _context;
        private readonly IMediator _mediator;

        public UpdateSaleCommandHandler(IBaseRepository<Sale> context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<BaseResponse<Sale>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _context.GetByIdAsync(request.SaleId, cancellationToken);

            if (sale == null)
            {
                return BaseResponse<Sale>.NewErro("ResourceNotFound", "Sale not found", $"The sale with ID {request.SaleId} does not exist in our database.");
            }
            if (sale.IsCancelled)
            {
                return BaseResponse<Sale>.NewErro("ValidationError", "Sale cancelled", $"The sale with ID {request.SaleId} is cancelled, cannot be changed.");
            }
            sale.Customer = request.Customer;
            sale.Branch = request.Branch;

            var oldTotal = sale.Total;
            var cancelleds = sale.SetItems(request.Items);

            var validate = sale.Validate();

            if (validate == null)
            {
                await _context.UpdateAsync(sale, cancellationToken);

                await _mediator.Publish(new SaleModifiedEvent(sale, oldTotal), cancellationToken);

                if (cancelleds.Any())
                {
                    await _mediator.Publish(new ItemCancelledEvent(sale, cancelleds), cancellationToken);
                }

                return BaseResponse<Sale>.New(sale);

            }

            return BaseResponse<Sale>.NewErro("ValidationError", "Invalid input data", validate);
        }
    }
}
