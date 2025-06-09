using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void Create_Should_Succeed_With_Valid_Parameters()
        {
            // Act
            var sale = SaleTestData.CreateValidSale();

            // Assert
            sale.Should().NotBeNull();
            sale.Status.Should().Be(SaleStatus.InProgress);
            sale.TotalAmount.Should().Be(0);
            sale.Items.Should().BeEmpty();
            sale.DomainEvents.Should().HaveCount(1);
            sale.DomainEvents.First().Should().BeOfType<SaleCreatedEvent>();
        }

        [Theory]
        [MemberData(nameof(SaleTestData.GetInvalidSaleCreationData), MemberType = typeof(SaleTestData))]
        public void Create_Should_Throw_DomainException_For_Invalid_Parameters(string saleNumber, Guid customerId, string customerName, Guid branchId, string branchName)
        {
            // Act
            Action act = () => new Sale(saleNumber, customerId, customerName, branchId, branchName);

            // Assert
            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void AddItem_Should_Add_New_Item_And_Recalculate_Total()
        {
            // Arrange
            var sale = SaleTestData.CreateValidSale();
            var productId = Guid.NewGuid();

            // Act
            sale.AddItem(productId, "Test Product", 2, 10, 10); // 10% discount

            // Assert
            sale.Items.Should().HaveCount(1);
            var addedItem = sale.Items.First();
            addedItem.ProductId.Should().Be(productId);
            addedItem.Quantity.Should().Be(2);
            addedItem.UnitPrice.Should().Be(10);
            addedItem.Total.Should().Be(18); // (2 * 10) * 0.9 = 18
            sale.TotalAmount.Should().Be(18);
        }

        [Fact]
        public void Cancel_Should_Change_Status_To_Cancelled()
        {
            // Arrange
            var sale = SaleTestData.CreateValidSale();

            // Act
            sale.Cancel();

            // Assert
            sale.Status.Should().Be(SaleStatus.Cancelled);
        }

        [Fact]
        public void Cancel_Should_Throw_DomainException_If_Already_Cancelled()
        {
            // Arrange
            var sale = SaleTestData.CreateValidSale();
            sale.Cancel();

            // Act
            Action act = () => sale.Cancel();

            // Assert
            act.Should().Throw<DomainException>().WithMessage("The sale has already been cancelled.");
        }

        [Fact]
        public void AddItem_Should_Throw_DomainException_If_Sale_Is_Cancelled()
        {
            // Arrange
            var sale = SaleTestData.CreateValidSale();
            sale.Cancel();

            // Act
            Action act = () => sale.AddItem(Guid.NewGuid(), "Product", 1, 10, 0);

            // Assert
            act.Should().Throw<DomainException>().WithMessage("It's not possible to add items to a cancelled sale.");
        }
    }
}
