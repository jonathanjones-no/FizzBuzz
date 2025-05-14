using FizzBuzzSolver.Controllers.FizzBuzz.Requests;
using FizzBuzzSolver.InternalServiceTests.Helpers;
using FluentAssertions;
using System.Net;
using Xunit.Abstractions;

namespace FizzBuzzSolver.InternalServiceTests.Controllers;
public class FizzBuzzControllerTests(FizzBuzzWebApplicationFactory<Program> application, ITestOutputHelper outputHelper) : BaseInternalServiceTest(application, outputHelper)
{
    private const string fizzBuzzBaseUrl = "/FizzBuzz";

    [Fact]
    public async Task Post_FizzBuzz_Succeeds()
    {

        FizzBuzzRequest request = new()
        {
            MaximumValue = 15,
            WordsByMultiples = new SortedDictionary<byte, string>
            {
                {5,  "Buzz"},
                {2, "Fizz"},
                {10, "Zap" }
            }
        };

        HttpClient client = _factory.CreateClient();
        HttpResponseMessage? response = await client.PostAsync(fizzBuzzBaseUrl, request.AsJsonHttpContent());
        string[]? results = response.ResponseAs<string[]>();
        string[] expected = ["0", "1", "Fizz", "3", "Fizz", "Buzz", "Fizz", "7", "Fizz", "9", "FizzBuzzZap", "11", "Fizz", "13", "Fizz", "Buzz"];
        results.Should().BeEquivalentTo(expected);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_FizzBuzz_MaximumValueTooHigh_Fails()
    {

        FizzBuzzRequest request = new()
        {
            MaximumValue = 251,
            WordsByMultiples = new SortedDictionary<byte, string>
            {
                {5,  "Buzz"},
                {2, "Fizz"},
                {10, "Zap" }
            }
        };

        HttpClient client = _factory.CreateClient();
        HttpResponseMessage? response = await client.PostAsync(fizzBuzzBaseUrl, request.AsJsonHttpContent());
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_FizzBuzz_EmptyRequest_Fails()
    {
        FizzBuzzRequest request = new();

        HttpClient client = _factory.CreateClient();
        HttpResponseMessage? response = await client.PostAsync(fizzBuzzBaseUrl, request.AsJsonHttpContent());
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
