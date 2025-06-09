using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    // Represents the query to get a specific sale by its ID.
    public class GetSaleByIdQuery : IRequest<GetSaleByIdQueryResult>
    {
        public Guid Id { get; set; }
    }
}
