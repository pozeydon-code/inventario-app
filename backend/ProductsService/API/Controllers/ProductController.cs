using Application.Products.Commands.Create;
using Application.Products.Commands.Delete;
using Application.Products.Commands.GetPagedProducts;
using Application.Products.Commands.Update;
using Application.Products.Commands.Update.UpdateStock;
using Application.Products.DTOs;
using Application.Products.Queries.GetProductById;

namespace API.Controllers;

[Route("api/products")]
public class Products : ApiController
{
    private readonly ISender _mediator;

    public Products(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductCommand commandRequest)
    {
        var createResult = await _mediator.Send(commandRequest);

        return createResult.Match(
            productId => Ok(productId),
            errors => Problem(errors)
            );
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
    {
        var result = await _mediator.Send(new GetPagedProductsQuery(page, pageSize, search));

        return result.Match(
            listResult => Ok(listResult),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));

        return result.Match(
            product => Ok(product),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateProductRequest productRequest)
    {

        UpdateProductCommand commandRequest = new UpdateProductCommand(id, productRequest.Name, productRequest.Description, productRequest.Category, productRequest.Image, productRequest.Price, productRequest.Stock);

        var updateResult = await _mediator.Send(commandRequest);

        return updateResult.Match(
            product => Ok(product),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteProductCommand(id));

        return deleteResult.Match(
            product => Ok(StatusCodes.Status200OK),
            errors => Problem(errors)
        );
    }

    [HttpPatch("{id}/stock")]
    public async Task<IActionResult> UpdateStock(Guid id, [FromBody] UpdateStock updateStock)
    {
        var updateResult = await _mediator.Send(new UpdateStockCommand(id, updateStock.Stock));

        return updateResult.Match(
            product => Ok(),
            errors => Problem(errors)
        );
    }
}
