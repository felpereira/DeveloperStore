using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    // Represents the command to create a new sale.
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string SaleNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public List<SaleItemCommand> Items { get; set; } = new();
    }

    // Represents a single item within the CreateSaleCommand.
    public class SaleItemCommand
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
