namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Advertisement;
using SecondHand.DataAccess.SqlServer.Api;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryDataAccess _categoryDataAccess;

    public CategoryController(ICategoryDataAccess categoryDataAccess)
    {
        _categoryDataAccess = categoryDataAccess;
    }

    [HttpGet()]
    public async Task<List<Category>> Get() => await Task.Run(() => _categoryDataAccess.GetCategory());

    [HttpGet("{id}")]
    public async Task<Category> Get(int id) => await Task.Run(() => _categoryDataAccess.GetCategory(id));
}
