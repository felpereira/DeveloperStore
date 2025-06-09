using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using System.Collections.Generic;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData
{
    public static class CreateSaleCommandHandlerTestData
    {
        public static CreateSaleCommand CreateValidCommand()
        {
            return new CreateSaleCommand
            {
                SaleNumber = "CMD-001",
                CustomerId = Guid.NewGuid(),
                CustomerName = "Command Customer",
                BranchId = Guid.NewGuid(),
                BranchName = "Command Branch",
                Items = new List<SaleItemCommand>
                {
                    new SaleItemCommand
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Command Product",
                        Quantity = 1,
                        UnitPrice = 100,
                        Discount = 0
                    }
                }
            };
        }
    }
}
