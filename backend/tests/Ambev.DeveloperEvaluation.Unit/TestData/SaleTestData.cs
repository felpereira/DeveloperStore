using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleTestData
    {
        public static IEnumerable<object[]> GetInvalidSaleCreationData()
        {
            yield return new object[] { "", Guid.NewGuid(), "Customer", Guid.NewGuid(), "Branch" }; // Invalid SaleNumber
            yield return new object[] { "123", Guid.Empty, "Customer", Guid.NewGuid(), "Branch" };   // Invalid CustomerId
            yield return new object[] { "123", Guid.NewGuid(), "Customer", Guid.Empty, "Branch" };   // Invalid BranchId
        }

        public static Sale CreateValidSale()
        {
            return new Sale("SALE-001", Guid.NewGuid(), "Test Customer", Guid.NewGuid(), "Test Branch");
        }
    }
}
