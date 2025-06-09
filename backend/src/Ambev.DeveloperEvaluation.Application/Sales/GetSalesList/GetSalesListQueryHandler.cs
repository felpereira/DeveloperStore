using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalesList
{
    // Handler for the GetSalesListQuery.
    public class GetSalesListQueryHandler : IRequestHandler<GetSalesListQuery, IEnumerable<SalesListItemDto>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSalesListQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalesListItemDto>> Handle(GetSalesListQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SalesListItemDto>>(sales);
        }
    }
}
