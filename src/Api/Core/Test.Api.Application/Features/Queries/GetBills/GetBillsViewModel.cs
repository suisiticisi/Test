namespace Test.Api.Application.Features.Queries.GetBills
{
    public class GetBillsViewModel
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal Discount { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
