using DeveloperStore.Domain.Commands;
using DeveloperStore.Domain.Infra;
using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Handlers
{
    public class SalesFilterCommandHandler : IRequestHandler<SalesFilterCommand, IEnumerable<Sale>>
    {
        private readonly IBaseRepository<Sale> _context;
        private readonly IMediator _mediator;

        public SalesFilterCommandHandler(IBaseRepository<Sale> context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IEnumerable<Sale>> Handle(SalesFilterCommand request, CancellationToken cancellationToken)
        {
            if(request.FilterBySaleId == null)
            {
                return await _context.GetByAllAsync(cancellationToken);
            }

            var resp = new List<Sale>();
            var sale = await _context.GetByIdAsync(request.FilterBySaleId.Value, cancellationToken);

            if(sale != null)
            {
                resp.Add(sale);
            }

            return resp;
        }
    }
}
