namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Advertisement;
using SecondHand.DataAccess.SqlServer.Interface;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductDataAccess _categoryDataAccess;

    public ProductController(IProductDataAccess categoryDataAccess) => _categoryDataAccess = categoryDataAccess;

    [HttpGet()]
    public async Task<List<Product>> Get() => await Task.Run(() => _categoryDataAccess.GetProduct());

    [HttpGet("{id}")]
    public async Task<Product> Get(int id) => await Task.Run(() => _categoryDataAccess.GetProduct(id));
}
