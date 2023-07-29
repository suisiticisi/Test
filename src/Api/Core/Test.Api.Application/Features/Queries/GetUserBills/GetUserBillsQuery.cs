using MediatR;
using Test.Api.Application.Features.Queries.GetBills;

namespace Test.Api.Application.Features.Queries.GetUserBills
{
    public class GetUserBillsQuery:IRequest<List<GetBillsViewModel>>
    {
        public GetUserBillsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }

    }
}
