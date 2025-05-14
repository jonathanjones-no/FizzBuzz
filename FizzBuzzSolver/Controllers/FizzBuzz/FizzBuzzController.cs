using FizzBuzzSolver.Controllers.FizzBuzz.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FizzBuzzSolver.Controllers.FizzBuzz;

/// <summary>
/// Controller for solving generic "FizzBuzz" problems
/// </summary>
/// <param name="logger"></param>
[ApiController]
[Route("[controller]")]
public class FizzBuzzController(ILogger<FizzBuzzController> logger) : ControllerBase
{
    private readonly ILogger<FizzBuzzController> _logger = logger;

    /// <summary>
    /// Controller for solving generic Fizz Buzz issues
    /// </summary>
    /// <param name="request">FizzBuzz request object</param>
    /// <returns>A string array containing the FizzBuzz result for a number at that number's index</returns>
    [HttpPost]
    public ActionResult<string[]> FizzBuzz([FromBody] FizzBuzzRequest request)
    {
        _logger.LogInformation("FizzBuzz Request Object: {request}", JsonSerializer.Serialize(request));

        if (request.WordsByMultiples.Count == 0)
            return Ok(Array.Empty<string>());

        string[] response = new string[request.MaximumValue + 1];
        response[0] = "0"; // Allows us to use index to get FizzBuzz for a specific number

        for (byte i = 1; i <= request.MaximumValue; i++)
        {
            Dictionary<byte, string> matches = request.WordsByMultiples.Where(k => i % k.Key == 0).ToDictionary();
            if (matches.Count > 0)
            {
                response[i] = string.Join("", matches.Select(kvp => kvp.Value));
            }
            else
            {
                response[i] = i.ToString();
            }
        }

        _logger.LogInformation("FizzBuzz Response Object: {response}", JsonSerializer.Serialize(response));
        return Ok(response);
    }
}
