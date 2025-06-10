using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        private Sale() { }

        public Sale(string saleNumber, Guid customerId, string customerName, Guid branchId, string branchName)
        {
            if (string.IsNullOrWhiteSpace(saleNumber))
                throw new DomainException("Sale number is required.");
            if (customerId == Guid.Empty)
                throw new DomainException("Customer identity is required.");
            if (branchId == Guid.Empty)
                throw new DomainException("Branch identity is required.");

            SaleNumber = saleNumber;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
            Date = DateTime.UtcNow;
            Status = SaleStatus.InProgress;
            _items = new List<SaleItem>();
            TotalAmount = 0;

            // Add the domain event to be dispatched later.
            AddDomainEvent(new SaleCreatedEvent(this.Id));
        }

        public string SaleNumber { get; private set; }
        public DateTime Date { get; private set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; }
        public SaleStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }

        private readonly List<SaleItem> _items;
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice, decimal discount)
        {
            if (Status == SaleStatus.Cancelled)
                throw new DomainException("It's not possible to add items to a cancelled sale.");

            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
                existingItem.UpdateQuantity(quantity);
            else
                _items.Add(new SaleItem(Id, productId, productName, quantity, unitPrice, discount));

            CalculateTotalAmount();
        }

        public void ClearItems()
        {
            _items.Clear();
            CalculateTotalAmount();
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = _items.Sum(item => item.Total);
        }

        public void Cancel()
        {
            if (Status == SaleStatus.Cancelled)
                throw new DomainException("The sale has already been cancelled.");

            Status = SaleStatus.Cancelled;
        }

        public void Complete()
        {
            if (Status != SaleStatus.InProgress)
                throw new DomainException("Only a sale in progress can be completed.");

            Status = SaleStatus.Completed;
        }
    }
}
