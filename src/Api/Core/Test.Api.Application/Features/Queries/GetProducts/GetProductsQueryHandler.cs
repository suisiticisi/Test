using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Api.Application.Interfaces.Repositories;

namespace Test.Api.Application.Features.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsViewModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProductsViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _productRepository.AsQueryable();
            query = query.Include(x => x.Category).OrderBy(x=>x.Category);

            var list = query.Select(x => new GetProductsViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Category = x.Category.Name
            });
            return list.ToList();
          
        }


    }
}
