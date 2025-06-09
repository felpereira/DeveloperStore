namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    // DTO for the request to create a new sale.
    public class CreateSaleRequest
    {
        public string SaleNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public List<SaleItemRequest> Items { get; set; } = new();
    }

    // DTO for an item within the sale request.
    public class SaleItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
