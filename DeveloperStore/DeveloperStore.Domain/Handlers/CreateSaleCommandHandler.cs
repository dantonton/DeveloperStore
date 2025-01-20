using DeveloperStore.Domain.Commands;
using DeveloperStore.Domain.Common;
using DeveloperStore.Domain.Events;
using DeveloperStore.Domain.Infra;
using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Handlers
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, BaseResponse<Sale>>
    {
        private readonly IBaseRepository<Sale> _context;
        private readonly IMediator _mediator;

        public CreateSaleCommandHandler(IBaseRepository<Sale> context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<BaseResponse<Sale>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale
            {
                SaleDate = DateTime.Now,
                Customer = request.Customer,
                Branch = request.Branch,
                Items = request.Items
            };

            var validate = sale.Validate();

            if (validate == null)
            {
                await _context.InsertAsync(sale, cancellationToken: cancellationToken);

                await _mediator.Publish(new SaleCreatedEvent(sale), cancellationToken);

                return BaseResponse<Sale>.New(sale);
            }

            return BaseResponse<Sale>.NewErro("ValidationError", "Invalid input data", validate);
        }
    }
}
