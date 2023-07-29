using MediatR;

namespace Test.Api.Application.Features.Queries.GetCategories
{
    public class GetCategoriesQuery:IRequest<List<GetCategoriesViewModel>>
    {

    }
}
