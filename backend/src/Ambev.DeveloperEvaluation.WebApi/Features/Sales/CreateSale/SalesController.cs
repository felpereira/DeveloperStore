using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
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
    }
}
