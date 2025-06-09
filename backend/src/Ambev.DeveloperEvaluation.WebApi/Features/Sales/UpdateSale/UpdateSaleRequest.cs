namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    // DTO for the request to update an existing sale.
    public class UpdateSaleRequest
    {
        // The items list will be taken from the request body.
        public List<UpdateSaleItemRequest> Items { get; set; } = new();
    }

    // DTO for an item within the update sale request.
    public class UpdateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
