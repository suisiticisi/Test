using MediatR;

namespace Test.Api.Application.Features.Queries.GetBillDetail
{
    public class GetBillDetailQuery:IRequest<GetBillDetailViewModel>
    {
        public GetBillDetailQuery(Guid userId, Guid billId)
        {
            UserId = userId;
            BillId = billId;
        }

        public Guid UserId { get; set; }
        public Guid BillId { get; set;}
    }
}
