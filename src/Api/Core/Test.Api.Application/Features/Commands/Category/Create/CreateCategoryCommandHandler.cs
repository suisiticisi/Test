using AutoMapper;
using MediatR;
using Test.Api.Application.Interfaces.Repositories;

namespace Test.Api.Application.Features.Commands.Category.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var dbCategory = mapper.Map<Domain.Models.Category>(request);
            await _categoryRepository.AddAsync(dbCategory);
            return dbCategory.Id;
        }


    }
}
