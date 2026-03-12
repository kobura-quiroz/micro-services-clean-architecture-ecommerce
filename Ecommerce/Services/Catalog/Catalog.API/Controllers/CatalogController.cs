using Catalog.Application.DTOs;
using Catalog.Application.Queries;
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Catalog.Application.Mappers;
using Catalog.Application.Commands;

namespace Catalog.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-all-products")]
    public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var query = new GetAllProductsQuery(catalogSpecParams);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("product-name/{productName}")]
    public async Task<ActionResult<IList<ProductDto>>> GetProductByProductName(string productName)
    {
        var query = new GetProductsByNameQuery(productName);
        var result = await _mediator.Send(query);
        if (result == null || !result.Any())
        {
            return NotFound();
        }
        var dtoList = result.Select(p => p.ToDto()).ToList();
        return Ok(dtoList);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var command = new DeleteProductByIdCommand(id);
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, UpdateProductDto updateProductDto)
    {
        var command = updateProductDto.ToCommand(id);
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("get-all-brands")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("get-all-types")]
    public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("/brand/{brand}", Name = "GetProductsByBrandName")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByBrand(string brand)
    {
        // First get the products
        var query = new GetProductsByBrandQuery(brand);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
