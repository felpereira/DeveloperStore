using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    // Represents the command to update an existing sale.
    public class UpdateSaleCommand : IRequest<Unit> // Unit represents a void return type
    {
        public Guid SaleId { get; set; }
        public List<UpdateSaleItemCommand> Items { get; set; } = new();
    }

    // Represents a single item within the UpdateSaleCommand.
    public class UpdateSaleItemCommand
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
