using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    // AutoMapper profile to map Sale domain entities to application-layer DTOs.
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, GetSaleByIdQueryResult>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<SaleItem, SaleItemResult>();
        }
    }
}
