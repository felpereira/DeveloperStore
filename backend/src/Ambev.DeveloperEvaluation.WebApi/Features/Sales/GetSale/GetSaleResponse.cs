namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    // DTO for the API response when getting a sale.
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemResponse> Items { get; set; } = new();
    }

    public class SaleItemResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}
