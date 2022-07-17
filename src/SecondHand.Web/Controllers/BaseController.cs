namespace SecondHand.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Api.Client;

public class BaseController : Controller
{
    protected readonly SecondHandApiClient SecondHandApiClient;

    public BaseController(IConfiguration configuration)
    {
        SecondHandApiClient = new SecondHandApiClient(
            string.Empty,
            string.Empty,
            configuration["SecondHandApiUrl"]!);
    }
}