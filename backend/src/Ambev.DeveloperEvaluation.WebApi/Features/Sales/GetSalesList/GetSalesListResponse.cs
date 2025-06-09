    namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSalesList
    {
        // DTO for an item in the sales list API response.
        public class GetSalesListResponse
        {
            public Guid Id { get; set; }
            public string SaleNumber { get; set; }
            public DateTime Date { get; set; }
            public string CustomerName { get; set; }
            public decimal TotalAmount { get; set; }
            public string Status { get; set; }
        }
    }
    