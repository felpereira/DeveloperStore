using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalesList
{
    // Query to get the list of all sales.
    public class GetSalesListQuery : IRequest<IEnumerable<SalesListItemDto>>
    {
    }

    // DTO for an item in the sales list.
    public class SalesListItemDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
