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

        app.MapGet("/logs", (ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("ProFlow.Api.Endpoints.Root");
            logger.LogInformation("GET /logs");

            var logFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "logs",
                $"proflow-{DateTime.Now:yyyyMMdd}.log");

            if (!File.Exists(logFilePath))
            {
                return Results.NotFound();
            }

            using var stream = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream);

            var entries = new List<string>(2);
            while (entries.Count < 2 && reader.ReadLine() is { } line)
            {
                entries.Add(line);
            }

            return Results.Ok(entries);
        });

        return app;
    }
}
