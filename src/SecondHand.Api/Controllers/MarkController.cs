namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Advertisement;
using SecondHand.DataAccess.SqlServer.Interface;

[ApiController]
[Route("api/[controller]")]
public class MarkController : BaseController
{
    private readonly IMarkDataAccess _categoryDataAccess;

    public MarkController(IMarkDataAccess categoryDataAccess,IConfiguration configuration) : base(configuration)
        => _categoryDataAccess = categoryDataAccess;

    [HttpGet()]
    public async Task<List<Mark>> Get() => await Task.Run(() => _categoryDataAccess.GetMark());

    [HttpGet("{id}")]
    public async Task<Mark> Get(int id) => await Task.Run(() => _categoryDataAccess.GetMark(id));
}
