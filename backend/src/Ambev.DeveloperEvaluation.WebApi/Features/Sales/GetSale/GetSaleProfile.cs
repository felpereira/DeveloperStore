using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    // AutoMapper profile to map application-layer results to API-layer responses.
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleByIdQueryResult, GetSaleResponse>();
            CreateMap<SaleItemResult, SaleItemResponse>();
        }
    }
}
