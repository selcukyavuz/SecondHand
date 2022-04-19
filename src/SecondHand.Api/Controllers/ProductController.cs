using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Adversitement;
using SecondHand.DataAccess.SqlServer.Api;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{   
    private readonly ILogger<ProductController> _logger;
    private readonly IProductDataAccess _categoryDataAccess;

    public ProductController(
        ILogger<ProductController> logger, 
        IProductDataAccess categoryDataAccess
        )
    {
        _logger = logger;
        _categoryDataAccess = categoryDataAccess;
    }

    [HttpGet()]
    public async Task<List<Product>> Get() => await Task.Run(() => _categoryDataAccess.GetProduct());

    [HttpGet("{id}")]
    public async Task<Product> Get(int id) => await Task.Run(() => _categoryDataAccess.GetProduct(id));

    
}
