using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    // Represents the command to cancel an existing sale.
    public class CancelSaleCommand : IRequest<Unit>
    {
        public Guid SaleId { get; set; }
    }
}
