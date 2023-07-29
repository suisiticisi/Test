using AutoMapper;
using MediatR;
using Test.Api.Application.Interfaces.Repositories;

namespace Test.Api.Application.Features.Queries.GetBills
{
    public class GetBillsQueryHandler : IRequestHandler<GetBillsQuery, List<GetBillsViewModel>>
    {
        private readonly IBillRepository _billRepository;
      

        public GetBillsQueryHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
            
        }

        public async Task<List<GetBillsViewModel>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
        {
            var query = _billRepository.AsQueryable();
            if (request.TodaysBill) { 
            
                //  query.Where(x=>x.CreateDate >= DateTime.UtcNow).Where(x=>x.CreateDate<=DateTime.Now.AddDays(-1));
                query.Where(x => x.CreateDate >= DateTime.UtcNow);
            }

            query=query.OrderBy(x=>x.CreateDate);

            var list= query.Select(x => new GetBillsViewModel()
            {
                Id =          x.Id,
                Discount =    x.Discount,
                FinalAmount = x.FinalAmount,
                TotalAmount = x.TotalAmount,
                CreatedDate=  x.CreateDate.Date

            }).ToList();

            return list;
        }

      

    }
}
