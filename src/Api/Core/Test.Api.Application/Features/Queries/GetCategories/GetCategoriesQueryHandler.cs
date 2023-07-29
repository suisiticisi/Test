using MediatR;
using Test.Api.Application.Interfaces.Repositories;

namespace Test.Api.Application.Features.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<GetCategoriesViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<GetCategoriesViewModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories=_categoryRepository.AsQueryable();
            var list = categories.Select(i => new GetCategoriesViewModel()
            {
                Id = i.Id,
               Name=i.Name,
               
            });
            return list.ToList() ;
        }
    }
}
