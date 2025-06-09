using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    // AutoMapper profile for mapping Create Sale related objects.
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            // Maps the request DTO to the application command.
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<SaleItemRequest, SaleItemCommand>();
            
            // Maps the application result to the response DTO.
            CreateMap<CreateSaleResult, CreateSaleResponse>();
        }
    }
}
