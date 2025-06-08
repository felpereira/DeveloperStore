using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    // The SaleItem class represents an item within a Sale.
    public class SaleItem : BaseEntity
    {
        // Constructor for EF Core.
        private SaleItem() { }

        // Constructor to create a new sale item.
        public SaleItem(Guid saleId, Guid productId, string productName, int quantity, decimal unitPrice, decimal discount)
        {
            // Validations to ensure the integrity of the item data.
            if (saleId == Guid.Empty)
                throw new DomainException("Sale identity is required.");
            if (productId == Guid.Empty)
                throw new DomainException("Product identity is required.");
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");
            if (unitPrice < 0)
                throw new DomainException("Unit price cannot be negative.");

            SaleId = saleId;
            ProductId = productId;
            ProductName = productName; // Denormalization of the product name.
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;

            // Calculates the item's total upon its creation.
            CalculateTotal();
        }

        // Sale Item's Properties.
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Total { get; private set; }

        // Method to update the quantity of an item.
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
            {
                throw new DomainException("Quantity must be greater than zero.");
            }
            Quantity = newQuantity;
            // Recalculates the item's total whenever the quantity is updated.
            CalculateTotal();
        }

        // Private method to calculate the item's total.
        private void CalculateTotal()
        {
            var totalBeforeDiscount = Quantity * UnitPrice;
            var discountAmount = totalBeforeDiscount * (Discount / 100);
            Total = totalBeforeDiscount - discountAmount;
        }
    }
}
