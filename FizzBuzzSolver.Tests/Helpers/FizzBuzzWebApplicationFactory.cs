using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace FizzBuzzSolver.InternalServiceTests.Helpers;
public class FizzBuzzWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    private ITestOutputHelper? _outputHelper;
    public void SetOutputHelper(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        Server.PreserveExecutionContext = true;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, __) => { });

        if (_outputHelper is null)
        {
            throw new InvalidOperationException("Please call .SetOutputHelper before configuring test host");
        }

        builder.ConfigureServices(services =>
        {
            services.AddLogging(logger =>
            {
                logger.ClearProviders();
                logger.AddXUnit(_outputHelper);
                logger.SetMinimumLevel(LogLevel.Information);
            });

            // Didn't get around to fully implementing auth
            services
                .AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, BasicUserNoPermissionsAuthHandler>("test", _ => { });
        });
    }
}
