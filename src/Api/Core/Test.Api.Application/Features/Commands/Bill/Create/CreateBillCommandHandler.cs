using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Api.Application.Enum;
using Test.Api.Application.Features.Queries.GetBills;
using Test.Api.Application.Interfaces.Repositories;
using Test.Api.Application.Utilities;
using Test.Api.Domain.Models;

namespace Test.Api.Application.Features.Commands.Bill.Create
{
    public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, GetBillsViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBillRepository _billRepository;
        private readonly IBillUserRelRepository _billUserRelRepository;
        private readonly IBillProductRelRepository _billProductRelRepository;
        private readonly IDiscountRepository _discountRepository;
        


        public CreateBillCommandHandler(IUserRepository userRepository, IProductRepository productRepository = null, IBillRepository billRepository = null, IBillUserRelRepository billUserRelRepository = null, IBillProductRelRepository billProductRelRepository = null, IDiscountRepository discountRepository = null, IRoleRepository roleRepository = null)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _billRepository = billRepository;

            _billUserRelRepository = billUserRelRepository;
            _billProductRelRepository = billProductRelRepository;
            _discountRepository = discountRepository;
            
        }

        public async Task<GetBillsViewModel> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            var userquery = _userRepository.AsQueryable().Include(u => u.Role).ThenInclude(x=>x.Discounts);
            var productquery = _productRepository.AsQueryable().Include(p => p.Category);

            decimal total = 0;
            decimal groceryTotal = 0;
            decimal exceptGroceryTotal = 0;
            decimal productPreise = 0;

            foreach (var productId in request.ProductIds)
            {

                //var product = await _productRepository.GetByIdAsync(productId);


                 var product = productquery.FirstOrDefault(x => x.Id == productId);

                if (product == null)
                {
                    throw new Exception("Product is not found");
                }
                var preis = product.Price;
                if (product.Category.Name == CategorieName.Grocerie.GetEnumDescription())
                {
                    groceryTotal += preis;
                }
                else
                {
                    exceptGroceryTotal += preis;
                }

                productPreise+= preis;
            }


            total = exceptGroceryTotal > 1000 ? exceptGroceryTotal * 0.95m + groceryTotal : exceptGroceryTotal + groceryTotal;


            var user = userquery.FirstOrDefault(x => x.Id == request.UserId);
            

            var discounts = _discountRepository.AsQueryable();
            

            decimal dicountRate = 0;
       

            string roleString = user.Role.Name;

           
            System.Enum.TryParse(roleString, out UserRoles role);
            
            
         
            switch (role)
            {

                case UserRoles.Employee:
                    dicountRate = discounts.FirstOrDefault(x => x.Role.Name == UserRoles.Employee.GetEnumDescription()).DiscountRate;

                    break;

                case UserRoles.Affiliate:
                    dicountRate = discounts.FirstOrDefault(x => x.Role.Name == UserRoles.Affiliate.GetEnumDescription()).DiscountRate;
                    break;

                case UserRoles.Customer:
                    dicountRate = discounts.FirstOrDefault(x => x.Role.Name == UserRoles.Customer.GetEnumDescription()).DiscountRate;

                    break;
                    
            }

            total = total - (total * (dicountRate / 100));

            var dicountPreice = productPreise - total;
            
            
            var bill = new Domain.Models.Bill()
            {

                TotalAmount = productPreise,
                CreateDate = DateTime.Now,
                Discount = productPreise - total,
                FinalAmount=total
            };
       
                    
          
            await _billRepository.AddAsync(bill);
                
            await _billUserRelRepository.AddAsync(new Domain.Models.BillUserRel() 
            {   BillId = bill.Id,
                CreateDate=DateTime.Now,
                UserId=request.UserId
            });


           foreach (var item in request.ProductIds)
           {
            await _billProductRelRepository.AddAsync(new Domain.Models.BillProductRel()
            {
             BillId = bill.Id,
             ProductId = item 
            });

           }

            var result = new GetBillsViewModel()
            {
                TotalAmount = productPreise,
                CreatedDate = DateTime.Now,
                Discount = productPreise - total,
                FinalAmount = total
            };    

            return result;
        }


        public async Task<GetBillsViewModel> HandleOrg(CreateBillCommand request, CancellationToken cancellationToken)
        {
            var userquery = _userRepository.AsQueryable().Include(u => u.Role).ThenInclude(x => x.Discounts);
            var productquery = _productRepository.AsQueryable().Include(p => p.Category);

            decimal total = 0;
            decimal groceryTotal = 0;
            decimal exceptGroceryTotal = 0;
            decimal productPreise = 0;

            foreach (var productId in request.ProductIds)
            {

                var product = productquery.FirstOrDefault(x => x.Id == productId);
                //var product = productquery.FirstOrDefault(x => x.Id == productId);
                //if (product==null)
                //{
                //    throw new Exception("Product ürün listesinde yok");
                //}
                var preis = product.Price;
                if (product.Category.Name == CategorieName.Grocerie.GetEnumDescription())
                {
                    groceryTotal += preis;
                }
                else
                {
                    exceptGroceryTotal += preis;
                }

                productPreise += preis;
            }


            total = exceptGroceryTotal > 1000 ? exceptGroceryTotal * 0.95m + groceryTotal : exceptGroceryTotal + groceryTotal;


            var user = userquery.FirstOrDefault(x => x.Id == request.UserId);


            var discounts = _discountRepository.AsQueryable();


            decimal dicountRate = 0;


            string roleString = user.Role.Name;


            System.Enum.TryParse(roleString, out UserRoles role);



            switch (role)
            {

                case UserRoles.Employee:
                    dicountRate = discounts.FirstOrDefault(x => x.Role.Name == UserRoles.Employee.GetEnumDescription()).DiscountRate;

                    break;

                case UserRoles.Affiliate:
                    dicountRate = discounts.FirstOrDefault(x => x.Role.Name == UserRoles.Affiliate.GetEnumDescription()).DiscountRate;
                    break;

                case UserRoles.Customer:
                    dicountRate = discounts.FirstOrDefault(x => x.Role.Name == UserRoles.Customer.GetEnumDescription()).DiscountRate;

                    break;

            }

            total = total - (total * (dicountRate / 100));

            var dicountPreice = productPreise - total;


            var bill = new Domain.Models.Bill()
            {

                TotalAmount = productPreise,
                CreateDate = DateTime.Now,
                Discount = productPreise - total,
                FinalAmount = total
            };



            await _billRepository.AddAsync(bill);

            await _billUserRelRepository.AddAsync(new Domain.Models.BillUserRel()
            {
                BillId = bill.Id,
                CreateDate = DateTime.Now,
                UserId = request.UserId
            });


            foreach (var item in request.ProductIds)
            {
                await _billProductRelRepository.AddAsync(new Domain.Models.BillProductRel()
                {
                    BillId = bill.Id,
                    ProductId = item
                });

            }

            var result = new GetBillsViewModel()
            {
                TotalAmount = productPreise,
                CreatedDate = DateTime.Now,
                Discount = productPreise - total,
                FinalAmount = total
            };

            return result;
        }
    }
}
