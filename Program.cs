var builder = WebApplication.CreateBuilder(args);
// register HTTP client for making external API calls
builder.Services.AddHttpClient();

var app = builder.Build();

// API key for external weather service
// set in user-secrets
var apiKey = builder.Configuration["VisualCrossing:ApiKey"]
             ?? Environment.GetEnvironmentVariable("VISUAL_CROSSING_API_KEY");

// API
app.MapGet("/weather/{city}", async (string city, HttpClient httpClient) =>
{
    var url =
        $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{Uri.EscapeDataString(city)}?unitGroup=metric&include=current&key={Uri.EscapeDataString(apiKey)}";

    // validate API key - null check
    if (string.IsNullOrWhiteSpace(apiKey))
        return Results.Problem("Visual Crossing API key not configured", statusCode: 500);


    // fetch weather data from Visual Crossing
    VisualCrossingResponse? resp;
    try
    {
        // make GET request and parse JSON response
        resp = await httpClient.GetFromJsonAsync<VisualCrossingResponse>(url);
    }
    catch
    {
        return Results.Problem("Error fetching weather data", statusCode: 502);
    }

    // validate response - null checks
    if (resp == null || resp.currentConditions == null)
        return Results.NotFound();

    var condition = resp.currentConditions.conditions;
    var temp = resp.currentConditions.temp;
    var weather = new Weather(resp.resolvedAddress, (int)Math.Round(temp), condition);

    return Results.Ok(weather);
});

app.Run();