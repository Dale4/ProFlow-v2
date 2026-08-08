namespace ProFlow.Api.Endpoints;

public static class WeatherForecastEndpoints
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public static IEndpointRouteBuilder MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast", (ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("ProFlow.Api.Endpoints.WeatherForecast");
                logger.LogInformation("GET /weatherforecast");

                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        Summaries[Random.Shared.Next(Summaries.Length)]
                    ))
                    .ToArray();

                logger.LogInformation("Returning {ForecastCount} weather forecasts", forecast.Length);
                return forecast;
            })
            .WithName("GetWeatherForecast");

        return app;
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
