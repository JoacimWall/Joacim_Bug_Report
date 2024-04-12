using System;
using System.Text.Json;
using GzipBug;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapPost("/v1/login", async (LoginRequest request)
   => await UserLogin(request))
    .Accepts<LoginRequest>("application/json")
   .Produces<UserDetails>(statusCode: 200, contentType: "application/json");

app.Run();
#region Functions
async Task<IResult> UserLogin(LoginRequest request)
{
    JsonSerializerOptions jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };
    LoginResponse result =new LoginResponse();
    using (StreamReader r = new StreamReader("loginResponse.json"))
    {
        string json = r.ReadToEnd();
        try
        {

        
        result =  JsonSerializer.Deserialize<LoginResponse>(json, jsonOptions);
        }
        catch (Exception ex)
        {

        }
    }
    return Results.Ok(result);
}
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

#endregion