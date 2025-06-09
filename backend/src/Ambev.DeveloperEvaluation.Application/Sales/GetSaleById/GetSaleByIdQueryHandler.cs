using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    // Handler for the GetSaleByIdQuery.
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, GetSaleByIdQueryResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSaleByIdQueryResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.FindByIdAsync(request.Id);

            if (sale == null)
            {
                // The controller will handle the null case and return a 404 Not Found.
                return null;
            }

            return _mapper.Map<GetSaleByIdQueryResult>(sale);
        }
    }
}
