namespace Test.Api.Application.Features.Queries.GetBillDetail
{
    public class GetBillDetailViewModel
    {
        public Guid Id { get;set; }

        public Guid BillId { get; set; }    
        public decimal TotalAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal Discount { get; set; }
        public List<ProductViewModel> Productss { get; set; } = new();
        public DateTime CreatedDate { get; set; }
    }

    public class ProductViewModel
    {
      
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public  string Category { get; set; }
    }
}
