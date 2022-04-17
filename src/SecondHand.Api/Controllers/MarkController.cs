using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Adversitement;
using SecondHand.DataAccess.SqlServer.Api;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarkController : ControllerBase
{   
    private readonly ILogger<MarkController> _logger;
    private readonly IMarkDataAccess _categoryDataAccess;

    public MarkController(
        ILogger<MarkController> logger, 
        IMarkDataAccess categoryDataAccess
        )
    {
        _logger = logger;
        _categoryDataAccess = categoryDataAccess;
    }

    [HttpGet()]
    public async Task<List<Mark>> Get() => await Task.Run(() => _categoryDataAccess.GetMark());

    [HttpGet("{id}")]
    public async Task<Mark> Get(int id) => await Task.Run(() => _categoryDataAccess.GetMark(id));

    
}
