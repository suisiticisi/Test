using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.Api.Application.Features.Commands.Bill.Create;
using Test.Api.Application.Features.Queries.GetBillDetail;
using Test.Api.Application.Features.Queries.GetBills;
using Test.Api.Application.Features.Queries.GetFinalBillAmount;
using Test.Api.Application.Features.Queries.GetUserBills;
using Test.Api.Domain.Models;
using Test.Api.WebApi.Controllers;

namespace TestProject
{
    public class BillApiControllerTest
    {
        private readonly BillController _billController;
        private readonly Mock<IMediator> _mockMediator;
     

        public BillApiControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _billController = new BillController(_mockMediator.Object);
         

        }


        [Theory]
        [InlineData("848238EE-8D29-4861-A992-08DB8EB71F34")]
        public async Task GetFinalBillAmount_Should_Return_Ok_With_Correct_Data(Guid billId)
        {

            var bills = new List<Bill>() { 
                new Bill()
            {
                Id= new Guid("848238EE-8D29-4861-A992-08DB8EB71F34") ,
                TotalAmount = 200,
                Discount = 60,
                FinalAmount = 140
            },
                new Bill()
            {
                Id= new Guid("0DC61796-E917-4F69-55D6-08DB8E4113FB") ,
                TotalAmount = 400,
                Discount = 120,
                FinalAmount = 280
            }
            };
            // Arrange

            var bill=bills.FirstOrDefault(x=>x.Id==billId);

            var expectedBill = new GetBillsViewModel
            {
                Id=bill.Id,
                TotalAmount = bill.TotalAmount,
                Discount = bill.Discount,
                FinalAmount = bill.FinalAmount
            };

            _mockMediator.Setup(m => m.Send(It.Is<GetFinalBillAmountQuery>(x=>x.BillId==billId),default(CancellationToken)))
                .ReturnsAsync(expectedBill);

            // Act
            var result = await _billController.GetFinalBillAmount(billId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualBill = Assert.IsType<GetBillsViewModel>(okResult.Value);

            Assert.Equal(expectedBill.TotalAmount, actualBill.TotalAmount);
            Assert.Equal(expectedBill.Discount, actualBill.Discount);
            Assert.Equal(expectedBill.FinalAmount, actualBill.FinalAmount);

        }


       
        [Fact]
        public async Task GetBills_Should_Return_OkResult() 
        {
            // Arrange

            var expectedBills = new List<GetBillsViewModel>
            {
                new GetBillsViewModel
                {
                    Id = new Guid("0DC61796-E917-4F69-55D6-08DB8E4113FB"),
                    TotalAmount = 200,
                    Discount = 10,
                    FinalAmount = 190,
                    CreatedDate = new DateTime(2023,7,29)
                },
                new GetBillsViewModel
                {
                    Id =new Guid("C70B917E-ED03-49C0-5B92-08DB8E4177E5"),
                    TotalAmount = 200,
                    Discount = 60,
                    FinalAmount = 140,
                    CreatedDate =  new DateTime(2023,7,29)
                }
            };

            //_mockMediator.Setup(m => m.Send(It.Is<GetBillsQuery>(q => q.TodaysBill == false), default(CancellationToken)))
            //    .ReturnsAsync(expectedBills);

            _mockMediator.Setup(m => m.Send(It.IsAny<GetBillsQuery>(), default(CancellationToken)))
                .ReturnsAsync(expectedBills);

            // Act
            var result = await _billController.GetBills(new GetBillsQuery());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualBills = Assert.IsType<List<GetBillsViewModel>>(okResult.Value);
            Assert.Equal(expectedBills.Count, actualBills.Count);

            for (int i = 0; i < expectedBills.Count; i++)
            {
                Assert.Equal(expectedBills[i].Id, actualBills[i].Id);
                Assert.Equal(expectedBills[i].TotalAmount, actualBills[i].TotalAmount);
                Assert.Equal(expectedBills[i].Discount, actualBills[i].Discount);
                Assert.Equal(expectedBills[i].FinalAmount, actualBills[i].FinalAmount);
                Assert.Equal(expectedBills[i].CreatedDate, actualBills[i].CreatedDate);
            }
        }


        [Theory]
        [InlineData("A3E5E7B6-BA05-493C-A63D-5F1EE641D361", "0DC61796-E917-4F69-55D6-08DB8E4113FB")] 
        public async Task GetBillDetail_Should_Return_OkResult(Guid userId,Guid billId)
        {

            var billuserrels = new List<BillUserRel>{
                new BillUserRel(){

                BillId = new Guid("0DC61796-E917-4F69-55D6-08DB8E4113FB"),
                UserId = new Guid("A3E5E7B6-BA05-493C-A63D-5F1EE641D361")
                },
                new BillUserRel(){

                BillId = new Guid("C70B917E-ED03-49C0-5B92-08DB8E4177E5"),
                UserId = new Guid("A3E5E7B6-BA05-493C-A63D-5F1EE641D361")
                },
            }; 
          

            var billDetail = new GetBillDetailViewModel()
                {
                    BillId = new Guid("0DC61796-E917-4F69-55D6-08DB8E4113FB"),
                    TotalAmount = 1300.00m,
                    Discount = 126.75m,
                    FinalAmount = 1173.25m,
                    CreatedDate = DateTime.UtcNow.Date,
                    Productss = new List<ProductViewModel>
                    {
                        new ProductViewModel
                        {
                            Category = "Grocerie",
                            Name = "Peynir",
                            Price = 300
                        },
                        new ProductViewModel
                        {
                            Category = "Genel",
                            Name = "Masa",
                            Price = 1000
                        }
                    }
                };

         

            _mockMediator.Setup(m => m.Send(It.Is<GetBillDetailQuery>(q => q.UserId == userId && q.BillId == billId), default(CancellationToken)))
                .ReturnsAsync(billDetail);

            // Act
            var result = await _billController.GetBillDetail(userId,billId);

            // Assert
            var createdOkResult = Assert.IsType<OkObjectResult>(result);
            var billDetailViewModel = Assert.IsType<GetBillDetailViewModel>(createdOkResult.Value);


            Assert.Equal(billId, billDetailViewModel.BillId);
            


        }


        [Theory]
        [InlineData("A3E5E7B6-BA05-493C-A63D-5F1EE641D361")]
        public async Task GetUserBills_Should_Return_OkResult(Guid userId) 
        {
            // Arrange
            var userBills = new List<GetBillsViewModel>
            {
                new GetBillsViewModel
                {
                    Id = Guid.NewGuid(),
                    TotalAmount = 200,
                    Discount = 60,
                    FinalAmount = 140,
                    CreatedDate = DateTime.UtcNow.Date
                },
                new GetBillsViewModel
                {
                    Id = Guid.NewGuid(),
                    TotalAmount = 600,
                    Discount = 180,
                    FinalAmount = 420,
                    CreatedDate = DateTime.UtcNow.Date
                }
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetUserBillsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userBills);

            // Act
            var result = await _billController.GetUserBills(userId);

            // Assert
           
            var createdOkResult = Assert.IsType<OkObjectResult>(result);
            var actualBills = Assert.IsAssignableFrom<List<GetBillsViewModel>>(createdOkResult.Value);

            Assert.Equal(2, actualBills.Count);


        }


        [Fact]
        public async Task CreateBill_Should_Return_OkResult()
        {
            // Arrange
            var command = new CreateBillCommand()
            {
                UserId = new Guid("3C296355-A97D-4046-AE86-998A045E05C6"),
                ProductIds = new List<Guid>() { new Guid("48D639BA-3C13-4F41-B16C-308360EBEF4C"), new Guid("CEE391D2-F8D0-4048-882E-9576EBEA5869") }

            };

            var model= new GetBillsViewModel()
            {
               
                TotalAmount = 200,
                Discount = 10,
                FinalAmount = 190,
                CreatedDate = DateTime.Today
            };

            _mockMediator.Setup(m => m.Send(It.Is<CreateBillCommand>(q => q.UserId == command.UserId && q.ProductIds == command.ProductIds), It.IsAny<CancellationToken>()))
                .ReturnsAsync(model);

            // Act
            var result = await _billController.CreateBill(command);

            // Assert
            var createdOkResult = Assert.IsType<OkObjectResult>(result);
            var actualTotal = Assert.IsType<GetBillsViewModel>(createdOkResult.Value);
         
            Assert.Equal(model.FinalAmount, actualTotal.FinalAmount);
            _mockMediator.Verify(x => x.Send(It.IsAny<CreateBillCommand>(), It.IsAny<CancellationToken>()), Times.Once);

        }

        

        [Fact]
        public async Task CreateBill_For_Employee_Affiliate_Customer_Success() 
        {
            //Arrange


            var products = new List<Product> {
                new Product()
                { Id = new Guid("67E39D74-0086-4C3D-96E2-0FB9F34070F0"), Price = 300, Category = new Category { Id = new Guid("5BD785FD-B4FA-4785-AD3C-733F25E2E622"), Name = "Grocerie", }
                },
                 new Product()
                { Id = new Guid("48D639BA-3C13-4F41-B16C-308360EBEF4C"), Price = 1000, Category = new Category { Id = new Guid("A2D218A1-A200-4768-819D-08DB8DD7EC4B"), Name = "Genel" }
                }

            };
            var productIds = products.Select(x => x.Id).ToList();

            var createBillCommandEmployee = new CreateBillCommand
            {
                UserId = new Guid("3C296355-A97D-4046-AE86-998A045E05C6"),
                ProductIds = productIds
            };

            var getBillsViewModelForEmployee = new GetBillsViewModel()
            {
                FinalAmount = 864.50m,
                Discount = 435.50m,
                TotalAmount = 1300.00m

            };

            var createBillCommandCustomer = new CreateBillCommand
            {
                UserId = new Guid("9D1B793A-EF08-474B-907D-FFB80691AADC"),
                ProductIds = productIds
            };

            var getBillsViewModelForCustomer = new GetBillsViewModel()
            {
                FinalAmount = 1173.25m,
                Discount = 126.75m,
                TotalAmount = 1300.00m

            };

            var createBillCommandAffiliate = new CreateBillCommand
            {
                UserId =Guid.NewGuid(),
                ProductIds = productIds
            };

            var getBillsViewModelForAffiliate = new GetBillsViewModel()
            {
                FinalAmount = 1111.50m,
                Discount = 188.50m,
                TotalAmount = 1300.00m

            };


            

            //Act

            _mockMediator.Setup(m => m.Send(It.Is<CreateBillCommand>(q => q.UserId == createBillCommandEmployee.UserId && q.ProductIds == createBillCommandEmployee.ProductIds), default(CancellationToken)))
            .ReturnsAsync(getBillsViewModelForEmployee);

            _mockMediator.Setup(m => m.Send(It.Is<CreateBillCommand>(q => q.UserId == createBillCommandAffiliate.UserId && q.ProductIds == createBillCommandAffiliate.ProductIds), default(CancellationToken)))
                .ReturnsAsync(getBillsViewModelForAffiliate);

            _mockMediator.Setup(m => m.Send(It.Is<CreateBillCommand>(q => q.UserId == createBillCommandCustomer.UserId && q.ProductIds == createBillCommandCustomer.ProductIds), default(CancellationToken)))
                .ReturnsAsync(getBillsViewModelForCustomer);


            var resultEmployee = await _billController.CreateBill(createBillCommandEmployee);
            var resultAffiliate = await _billController.CreateBill(createBillCommandAffiliate);
            var resultCustomer = await _billController.CreateBill(createBillCommandCustomer);

            var createdOkResultEmployee = Assert.IsType<OkObjectResult>(resultEmployee);
            var returngetBillsViewModelEmployeee = Assert.IsType<GetBillsViewModel>(createdOkResultEmployee.Value);

            var createdOkResultAffiliate = Assert.IsType<OkObjectResult>(resultAffiliate);
            var returngetBillsViewModelAffiliate = Assert.IsType<GetBillsViewModel>(createdOkResultAffiliate.Value);

            var createdOkResultCustomer = Assert.IsType<OkObjectResult>(resultCustomer);
            var returngetBillsViewModelCustomer = Assert.IsType<GetBillsViewModel>(createdOkResultCustomer.Value);



            // Assert

            Assert.Equal(864.50m, returngetBillsViewModelEmployeee.FinalAmount);
            Assert.Equal(435.50m, returngetBillsViewModelEmployeee.Discount);
            Assert.Equal(1300.00m, returngetBillsViewModelEmployeee.TotalAmount);

            Assert.Equal(1111.50m, returngetBillsViewModelAffiliate.FinalAmount);
            Assert.Equal(188.50m, returngetBillsViewModelAffiliate.Discount);
            Assert.Equal(1300.00m, returngetBillsViewModelAffiliate.TotalAmount);

            Assert.Equal(1173.25m, returngetBillsViewModelCustomer.FinalAmount);
            Assert.Equal(126.75m,  returngetBillsViewModelCustomer.Discount);
            Assert.Equal(1300.00m, returngetBillsViewModelCustomer.TotalAmount);

            
        }

    }

}

