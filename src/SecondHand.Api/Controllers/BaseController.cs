namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

public class BaseController : ControllerBase
{
    private const string _variable = "RABBITCONNECTION";
    private const string _rabbitSettingKey = "RabbitSettings";
    private const string _connectionKey = "Connection";
    protected readonly string ConnectionString;
    public BaseController(IConfiguration configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        ConnectionString = Environment.GetEnvironmentVariable(_variable)
                            ??
                            configuration.GetSection(_rabbitSettingKey).GetSection(_connectionKey).Value;
    }
}