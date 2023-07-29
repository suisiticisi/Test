﻿namespace Test.Api.Application.Features.Queries.GetProducts
{
    public class GetProductsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }    

        public decimal Price { get; set; }
    }
}
