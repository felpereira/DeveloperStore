// using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    // Handler for the UpdateSaleCommand.
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DbContext _context;

        public UpdateSaleCommandHandler(ISaleRepository saleRepository, DbContext context)
        {
            _saleRepository = saleRepository;
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.FindByIdAsync(request.SaleId);

            if (sale == null)
            {
                // This exception can be caught by a middleware to return a 404 Not Found.
                throw new DomainException("Venda não encontrada.");
            }

            // A simple approach to update items: clear the existing list and add the new ones.
            // EF Core will track the changes (deletions and insertions).
            // sale.ClearItems();
            
            foreach (var itemCommand in request.Items)
            {
                sale.AddItem(
                    itemCommand.ProductId,
                    itemCommand.ProductName,
                    itemCommand.Quantity,
                    itemCommand.UnitPrice,
                    itemCommand.Discount
                );
            }

            // The repository's update method marks the entity state as Modified.
            await _saleRepository.UpdateAsync(sale);
            
            // Persist all tracked changes to the database.
            await _context.SaveChangesAsync(cancellationToken);

            // Return Unit.Value to signify a successful, void operation.
            return Unit.Value;
        }
    }
}
