using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    // Handler for the CreateSaleCommand.
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DbContext _context;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, DbContext context)
        {
            _saleRepository = saleRepository;
            _context = context;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            // Create the main Sale entity from the command data.
            var sale = new Sale(
                request.SaleNumber,
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName
            );

            // Add each item from the command to the sale entity.
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

            // Add the new sale to the repository.
            await _saleRepository.AddAsync(sale);

            // Save the changes to the database.
            await _context.SaveChangesAsync(cancellationToken);

            // Return the result containing the new sale's ID.
            return new CreateSaleResult { SaleId = sale.Id };
        }
    }
}
