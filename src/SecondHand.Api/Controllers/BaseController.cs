namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

public class BaseController : ControllerBase
{
    protected readonly string ConnectionString;
    public BaseController(IConfiguration configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        ConnectionString = Environment.GetEnvironmentVariable("RABBITCONNECTION")
                            ??
                            configuration.GetSection("RabbitSettings").GetSection("Connection").Value;
    }
}