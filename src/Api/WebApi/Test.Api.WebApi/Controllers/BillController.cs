using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test.Api.Application.Features.Commands.Bill.Create;
using Test.Api.Application.Features.Queries.GetBillDetail;
using Test.Api.Application.Features.Queries.GetBills;
using Test.Api.Application.Features.Queries.GetFinalBillAmount;
using Test.Api.Application.Features.Queries.GetUserBills;

namespace Test.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public BillController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("CreateBill")]
        public async Task<IActionResult> CreateBill([FromBody] CreateBillCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }



        [HttpGet]
        [Route("GetFinalBillAmount{billId}")]
        public async Task<IActionResult> GetFinalBillAmount(Guid billId)
        {
            var result = await mediator.Send(new GetFinalBillAmountQuery(billId));

            return Ok(result);
        }



        [HttpGet]
        [Route("GetBills")]
        public async Task<IActionResult> GetBills([FromQuery] GetBillsQuery query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }


        [HttpGet]
        [Route("GetUserBills/{userid}")]
        public async Task<IActionResult> GetUserBills(Guid userid)
        {
            var result = await mediator.Send(new GetUserBillsQuery(userid));

            return Ok(result);
        }

        [HttpGet]
        [Route("GetBillDetail/{userid}/{billId}")]
        public async Task<IActionResult> GetBillDetail(Guid userid,Guid billId)
        {
            var result = await mediator.Send(new GetBillDetailQuery(userid, billId));

            return Ok(result);
        }



    }
}
