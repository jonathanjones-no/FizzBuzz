using System.ComponentModel.DataAnnotations;

namespace FizzBuzzSolver.Controllers.FizzBuzz.Requests;
/// <summary>
/// The request to solve a generic FizzBuzz problem
/// </summary>
public class FizzBuzzRequest
{
    /// <summary>
    /// The maximum number that we can count to (and subsequently the maximum number we can have in our dictionary)
    /// </summary>
    [Range(1, 250)]
    public byte MaximumValue { get; set; }
    /// <summary>
    /// A dictionary that maps our multiples to the word that should be output
    /// </summary>
    public SortedDictionary<byte, string> WordsByMultiples { get; set; } = [];

}
