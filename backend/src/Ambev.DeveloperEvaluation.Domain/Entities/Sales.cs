using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    // The Sale class represents the aggregate root for a Sale.
    public class Sale : BaseEntity
    {
        // Constructor for EF Core.
        private Sale() { }

        // Constructor to create a new sale.
        public Sale(string saleNumber, Guid customerId, string customerName, Guid branchId, string branchName)
        {
            // Input parameter validation to ensure data consistency.
            if (string.IsNullOrWhiteSpace(saleNumber))
                throw new DomainException("Sale number is required.");
            if (customerId == Guid.Empty)
                throw new DomainException("Customer identity is required.");
            if (branchId == Guid.Empty)
                throw new DomainException("Branch identity is required.");

            SaleNumber = saleNumber;
            CustomerId = customerId;
            CustomerName = customerName; // Denormalization of the customer's name.
            BranchId = branchId;
            BranchName = branchName; // Denormalization of the branch's name.
            Date = DateTime.UtcNow;
            Status = SaleStatus.InProgress; // A new sale starts with the "InProgress" status.
            _items = new List<SaleItem>();
            TotalAmount = 0;
        }

        // Sale Properties.
        public string SaleNumber { get; private set; }
        public DateTime Date { get; private set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; }
        public SaleStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }

        // Private field for the collection of sale items.
        private readonly List<SaleItem> _items;
        // Exposing the collection of items as IReadOnlyCollection to protect the list from external modifications.
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        // Method to add an item to the sale.
        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice, decimal discount)
        {
            // Business rule: it's not possible to add items to a cancelled sale.
            if (Status == SaleStatus.Cancelled)
            {
                throw new DomainException("It's not possible to add items to a cancelled sale.");
            }

            // Checks if the item already exists in the sale.
            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                // If the item already exists, updates the quantity.
                existingItem.UpdateQuantity(quantity);
            }
            else
            {
                // Otherwise, adds a new item to the list.
                var newItem = new SaleItem(Id, productId, productName, quantity, unitPrice, discount);
                _items.Add(newItem);
            }

            // Recalculates the total amount of the sale whenever an item is added or updated.
            CalculateTotalAmount();
        }

        // Method to calculate the total amount of the sale.
        public void CalculateTotalAmount()
        {
            TotalAmount = _items.Sum(item => item.Total);
        }

        // Method to cancel the sale.
        public void Cancel()
        {
            // Business rule: it's not possible to cancel a sale that is already cancelled.
            if (Status == SaleStatus.Cancelled)
            {
                throw new DomainException("The sale has already been cancelled.");
            }
            Status = SaleStatus.Cancelled;
        }

        // Method to mark the sale as completed.
        public void Complete()
        {
             if (Status != SaleStatus.InProgress)
            {
                throw new DomainException("Only a sale in progress can be completed.");
            }
            Status = SaleStatus.Completed;
        }
    }
}
