namespace Ambev.DeveloperEvaluation.Domain.Enums
{
    // Enum to represent the possible statuses of a Sale.
    public enum SaleStatus
    {
        // The sale has started but has not yet been completed.
        InProgress = 1,

        // The sale was completed successfully.
        Completed = 2,

        // The sale has been cancelled.
        Cancelled = 3
    }
}
