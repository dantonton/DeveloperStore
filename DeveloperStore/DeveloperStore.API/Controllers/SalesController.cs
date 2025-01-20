using AutoMapper;
using DeveloperStore.API.Models.Sales;
using DeveloperStore.Domain.Commands;
using DeveloperStore.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DeveloperStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {

        private readonly ILogger<SalesController> _logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SalesController(ILogger<SalesController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet()]
        public async Task<IEnumerable<Sale>> Get()
        {
            var command = new SalesFilterCommand();
            return await mediator.Send(command); 
        }

        [HttpGet("{saleNumber}")]
        public async Task<Sale?> Get(Guid saleNumber)
        {
            var command = new SalesFilterCommand();
            command.FilterBySaleId = saleNumber;
            var resp = await mediator.Send(command);
            return resp.FirstOrDefault();
        }


        [HttpPost()]
        public async Task<ActionResult<Sale>> Post(SalePostRequest sale)
        {
            try
            {
                var command = mapper.Map<CreateSaleCommand>(sale);
                var resp = await mediator.Send(command);
                if(resp.IsSuccess) {
                    return Ok(resp.Data);
                }
                return BadRequest(new
                {
                    type = resp.TypeErro,
                    error = resp.Erro,
                    detail = resp.Detail
                });
            }
            catch (Exception ex)
            {
                return Problem(type: "Exception",
                    title: ex.Message,
                    detail:"Error with our servers.");
            }


        }


        [HttpPut()]
        public async Task<ActionResult<Sale>> Put(SalePutRequest sale)
        {
            try
            {
                var command = mapper.Map<UpdateSaleCommand>(sale);
                var resp = await mediator.Send(command);
                if (resp.IsSuccess)
                {
                    return Ok(resp.Data);
                }
                return BadRequest(new
                {
                    type = resp.TypeErro,
                    error = resp.Erro,
                    detail = resp.Detail
                });
            }
            catch (Exception ex)
            {
                return Problem(type: "Exception",
                    title: ex.Message,
                    detail: "Error with our servers.");
            }
        }


        [HttpDelete("{saleNumber}")]
        public async Task<ActionResult<Sale>> Delete(Guid saleNumber)
        {
            try
            {
                var command = new SalesCancelCommand
                {
                    SaleId = saleNumber
                };
                var resp = await mediator.Send(command);
                if (resp.IsSuccess)
                {
                    return Ok(resp.Data);
                }
                return BadRequest(new
                {
                    type = resp.TypeErro,
                    error = resp.Erro,
                    detail = resp.Detail
                });
            }
            catch (Exception ex)
            {
                return Problem(type: "Exception",
                    title: ex.Message,
                    detail: "Error with our servers.");
            }
        }
    }
}
