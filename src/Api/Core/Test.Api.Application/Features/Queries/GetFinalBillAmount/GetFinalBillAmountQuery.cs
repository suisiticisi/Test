using MediatR;
using Test.Api.Application.Features.Queries.GetBills;

namespace Test.Api.Application.Features.Queries.GetFinalBillAmount
{
    public class GetFinalBillAmountQuery:IRequest<GetBillsViewModel>
    {
        public GetFinalBillAmountQuery(Guid billId)
        {
            BillId = billId;
        }

        public Guid BillId { get; set; }
    }
}
