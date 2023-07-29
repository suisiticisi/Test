using MediatR;
using Test.Api.Application.Features.Queries.GetBills;

namespace Test.Api.Application.Features.Commands.Bill.Create
{
    public class CreateBillCommand:IRequest<GetBillsViewModel>
    {
        public Guid UserId { get; set; }
        
        public List<Guid> ProductIds { get; set; }=new List<Guid>();

    }
}
