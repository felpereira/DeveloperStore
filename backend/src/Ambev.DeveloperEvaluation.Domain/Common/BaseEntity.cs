using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; }

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }

     // Private list to hold domain events.
        private readonly List<IDomainEvent> _domainEvents = new();

        // Public read-only collection of domain events.
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        // Method to add a domain event to the entity.
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        // Method to clear all domain events from the entity.
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
}
