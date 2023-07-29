using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Api.Application.Interfaces.Repositories;

namespace Test.Api.Application.Features.Queries.GetBillDetail
{
    public class GetBillDetailQueryHandler : IRequestHandler<GetBillDetailQuery, GetBillDetailViewModel>
    {
        private readonly IBillRepository _billRepository;
        private readonly IProductRepository _productRepository;
   

        public GetBillDetailQueryHandler(IBillRepository billRepository, IProductRepository productRepository)
        {
            
            _billRepository = billRepository;
            _productRepository = productRepository;
        }

      
        public async Task<GetBillDetailViewModel> Handle(GetBillDetailQuery request, CancellationToken cancellationToken)
        {

            var query = _billRepository.AsQueryable()
                .Include(_ => _.BillProductRels).ThenInclude(_ => _.Product).ThenInclude(_ => _.Category)
                .Include(_ => _.BillUserRels).ThenInclude(_ => _.User).ToList();

            var bills = query.Where(
                x => x.BillUserRels.Where(y => y.UserId == request.UserId && y.BillId == request.BillId).Select(y => y.UserId).Contains((request.UserId)));

          

            var productIds = bills.Select(
                x => x.BillProductRels.Select(x => x.ProductId))
                .First();

           
            var produsts = _productRepository.AsQueryable().Where(x => productIds.Contains(x.Id));

            var productViewModels = produsts.Select(x => new ProductViewModel()
            {
                Category = x.Category.Name,
                Name = x.Name,
                Price = x.Price,

            }).ToList();

            var billDb = bills.FirstOrDefault(x => x.Id == request.BillId);

            var billDetailViewModel = new GetBillDetailViewModel()
            {
                BillId = billDb.Id,
                TotalAmount = billDb.TotalAmount,
                Discount = billDb.Discount,
                FinalAmount = billDb.FinalAmount,
                CreatedDate = DateTime.Now,
                Productss = productViewModels
            };
         

            return billDetailViewModel;
        }
    }
}
