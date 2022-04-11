namespace SecondHand.Web;

public static class HttpContextExtensions
{
    public static void AddHttpContextAccessor(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
    {
        MyHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
        return app;
    }
}