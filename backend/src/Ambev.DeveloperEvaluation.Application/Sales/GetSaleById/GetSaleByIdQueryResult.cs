namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    // Represents the data structure returned by the GetSaleByIdQuery.
    public class GetSaleByIdQueryResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemResult> Items { get; set; } = new();
    }

    public class SaleItemResult
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}
