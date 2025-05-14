using Microsoft.AspNetCore.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FizzBuzzSolver.InternalServiceTests.Helpers;

public static class TestHelpers
{
    public static HttpContent AsJsonHttpContent<T>(this T content)
    {
        JsonOptions options = new();
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.SerializerOptions.PropertyNameCaseInsensitive = true;
        string? json = JsonSerializer.Serialize(content, options.SerializerOptions);

        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    public static T ResponseAs<T>(this HttpResponseMessage message)
    {
        JsonOptions options = new();
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.SerializerOptions.PropertyNameCaseInsensitive = true;

        return JsonSerializer.Deserialize<T>(message.Content.ReadAsStream(), options.SerializerOptions)
            ?? throw new NullReferenceException("The result of deserialization was null");
    }
}

[CollectionDefinition(nameof(SerialCollection), DisableParallelization = true)]
public class SerialCollection : ICollectionFixture<FizzBuzzWebApplicationFactory<Program>>
{
    // Doesn't need code, only exists to produce XUnit collection fixtures for serial test execution
}
