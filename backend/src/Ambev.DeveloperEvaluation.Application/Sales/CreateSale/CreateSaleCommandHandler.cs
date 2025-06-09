using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DbContext _context;
        private readonly IPublisher _publisher;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, DbContext context, IPublisher publisher)
        {
            _saleRepository = saleRepository;
            _context = context;
            _publisher = publisher;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale(
                request.SaleNumber,
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName
            );

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

            await _saleRepository.AddAsync(sale);
            await _context.SaveChangesAsync(cancellationToken);

            // Publish domain events after saving changes.
            foreach (var domainEvent in sale.DomainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return new CreateSaleResult { SaleId = sale.Id };
        }
    }
}
