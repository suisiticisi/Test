using MediatR;


namespace Test.Api.Application.Features.Commands.Category.Create
{
    public class CreateCategoryCommand:IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
