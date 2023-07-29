using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Api.Application.Features.Queries.GetBills;
using Test.Api.Application.Interfaces.Repositories;

namespace Test.Api.Application.Features.Queries.GetUserBills
{
    public class GetUserBillsQueryHandler : IRequestHandler<GetUserBillsQuery, List<GetBillsViewModel>>
    {
        private readonly IBillUserRelRepository _billUserRelRepository;

        public GetUserBillsQueryHandler(IBillUserRelRepository billUserRelRepository)
        {
            _billUserRelRepository = billUserRelRepository;
        }

        public async Task<List<GetBillsViewModel>> Handle(GetUserBillsQuery request, CancellationToken cancellationToken)
        {
            var query = _billUserRelRepository.AsQueryable().Include(x => x.Bill);
            var bills=query.Where(x=>x.UserId == request.UserId);

            var userBills = bills.Select(x => new GetBillsViewModel()
            {
                CreatedDate = x.CreateDate,
                Id = x.BillId,
                Discount = x.Bill.Discount,
                FinalAmount = x.Bill.FinalAmount,
                TotalAmount = x.Bill.TotalAmount,

            });

            return userBills.ToList();
        }
    }
}
