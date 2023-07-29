using MediatR;

namespace Test.Api.Application.Features.Queries.GetBills
{
    public class GetBillsQuery:IRequest<List<GetBillsViewModel>>
    {
        public bool TodaysBill { get; set; } 

        public int Count { get; set; } 
    }
}
