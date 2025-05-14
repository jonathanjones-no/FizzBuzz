using FizzBuzzSolver.InternalServiceTests.Helpers;
using Xunit.Abstractions;

namespace FizzBuzzSolver.InternalServiceTests;

[Collection(nameof(SerialCollection))]
public abstract class BaseInternalServiceTest
{
    protected readonly FizzBuzzWebApplicationFactory<Program> _factory;

    protected BaseInternalServiceTest(FizzBuzzWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        factory.SetOutputHelper(testOutputHelper);
    }
}
