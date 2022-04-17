using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Adversitement;
using SecondHand.DataAccess.SqlServer.Api;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{   
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryDataAccess _categoryDataAccess;

    public CategoryController(
        ILogger<CategoryController> logger, 
        ICategoryDataAccess categoryDataAccess
        )
    {
        _logger = logger;
        _categoryDataAccess = categoryDataAccess;
    }

    [HttpGet()]
    public async Task<List<Category>> Get() => await Task.Run(() => _categoryDataAccess.GetCategory());

    [HttpGet("{id}")]
    public async Task<Category> Get(int id) => await Task.Run(() => _categoryDataAccess.GetCategory(id));

    
}
