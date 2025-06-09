using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [Route("api/sales")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="request">The data for the new sale.</param>
        /// <returns>The ID of the newly created sale.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
        {
            // Map the request DTO to the application command.
            var command = _mapper.Map<CreateSaleCommand>(request);

            // Send the command to the handler and await the result.
            // Using "this.Mediator" to resolve ambiguity with the MediatR class.
            var result = await _mediator.Send(command);

            // Map the result to the response DTO.
            var response = _mapper.Map<CreateSaleResponse>(result);


            // Create the response object using the parameterless constructor
            // and then set the Data property.
            var apiResponse = new ApiResponseWithData<CreateSaleResponse>
            {
                Data = response
            };

            // Return a "201 Created" response with the location of the new resource
            // and the structured API response.
            return CreatedAtAction(nameof(CreateSale), new { id = response.SaleId }, apiResponse);
        }
        
        /// <summary>
        /// Retrieves a specific sale by its unique identifier.
        /// </summary>
        /// <param name="id">The sale's unique identifier.</param>
        /// <returns>The details of the sale.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            var query = new GetSaleByIdQuery { Id = id };
            var result = await _mediator.Send(query);


            if (result == null)
            {
                return NotFound(new ApiResponse(){Message = "Venda não encontrada."});
            }
            
            var response = _mapper.Map<GetSaleResponse>(result);
            return Ok(new ApiResponseWithData<GetSaleResponse> { Data = response });
        }
    }
}
