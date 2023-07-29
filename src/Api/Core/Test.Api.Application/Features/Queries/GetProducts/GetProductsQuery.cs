using MediatR;

namespace Test.Api.Application.Features.Queries.GetProducts
{
    public class GetProductsQuery:IRequest<List<GetProductsViewModel>>
    {
    }
}
