using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FizzBuzzSolver.InternalServiceTests.Helpers;

/// <summary>
/// I had planned on adding auth but didn't have time, leaving the testing implementations though because I did those first.
/// </summary>
/// <param name="options"></param>
/// <param name="logger"></param>
/// <param name="encoder"></param>
public class BasicUserNoPermissionsAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options, 
    ILoggerFactory logger, 
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options,logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Claim[] claims =
        [
            new Claim("userId", "10")
        ];

        ClaimsIdentity identity = new(claims, "fizzBuzzAuth");
        ClaimsPrincipal principal = new(identity);
        AuthenticationTicket ticket = new(principal, "test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

public class UnauthenticatedAuth(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.Fail("You are not authenticated"));
    }
}
