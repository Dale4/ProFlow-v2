namespace ProFlow.Api.Endpoints;

public static class RootEndpoints
{
    public static IEndpointRouteBuilder MapRootEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("ProFlow.Api.Endpoints.Root");
            logger.LogInformation("GET /");
            return "Hello World";
        });
        return app;
    }
}
