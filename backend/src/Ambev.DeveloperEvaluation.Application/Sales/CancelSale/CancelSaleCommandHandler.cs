using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    // Handler for the CancelSaleCommand.
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DbContext _context;

        public CancelSaleCommandHandler(ISaleRepository saleRepository, DbContext context)
        {
            _saleRepository = saleRepository;
            _context = context;
        }

        public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.FindByIdAsync(request.SaleId);

            if (sale == null)
            {
                throw new DomainException("Venda não encontrada.");
            }

            // Call the domain method to perform the cancellation logic.
            sale.Cancel();

            await _saleRepository.UpdateAsync(sale);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
