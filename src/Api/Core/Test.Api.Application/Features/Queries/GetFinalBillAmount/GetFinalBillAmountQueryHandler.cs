using MediatR;
using Test.Api.Application.Features.Queries.GetBills;
using Test.Api.Application.Interfaces.Repositories;

namespace Test.Api.Application.Features.Queries.GetFinalBillAmount
{
    public class GetFinalBillAmountQueryHandler : IRequestHandler<GetFinalBillAmountQuery, GetBillsViewModel>
    {
        private readonly IBillRepository _billRepository;

        public GetFinalBillAmountQueryHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<GetBillsViewModel> Handle(GetFinalBillAmountQuery request, CancellationToken cancellationToken)
        {

           
            var dbBill=await _billRepository.GetByIdAsync(request.BillId);

            var bill = new GetBillsViewModel()
            {
                Id = dbBill.Id,
                TotalAmount = dbBill.TotalAmount,
                Discount = dbBill.Discount,
                FinalAmount = dbBill.FinalAmount,
                CreatedDate=dbBill.CreateDate
            };


            return bill;
        }
    }
   
    
}
