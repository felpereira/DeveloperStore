using Ambev.DeveloperEvaluation.Application.Sales.GetSalesList;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSalesList
{
    // AutoMapper profile to map application-layer DTOs to API-layer responses.
    public class GetSalesListProfile : Profile
    {
        public GetSalesListProfile()
        {
            CreateMap<SalesListItemDto, GetSalesListResponse>();
        }
    }
}
