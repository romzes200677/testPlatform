using CSharpTestApp.Models;

namespace CSharpTestApp.Infrastructure;

public interface ITestRepository
{
    public Task<List<UnitTest>> GetTestsForAssignment(string assignmentId);
}

public class InMemoryTestRepository : ITestRepository
{
    private readonly Dictionary<string, List<UnitTest>> _assignments = new()
    {
        ["addition-01"] = new List<UnitTest>
        {
            new UnitTest
            {
                Name = "Addition of positive numbers",
                MethodName = "Add",
                Parameters = new object[] { 2, 3 },
                ExpectedResult = 5
            },
            new UnitTest
            {
                Name = "Addition with zero",
                MethodName = "Add",
                Parameters = new object[] { 5, 0 },
                ExpectedResult = 5
            }
        },
        
        ["calculator"] = new List<UnitTest>
        {
            new UnitTest
            {
                Name = "Simple division",
                MethodName = "Divide",
                Parameters = new object[] { 10, 2 },
                ExpectedResult = 5
            }
        }
    };

    public Task<List<UnitTest>> GetTestsForAssignment(string assignmentId)
    {
        if (_assignments.TryGetValue(assignmentId, out var tests))
        {
            return Task.FromResult(tests);
        }
        return Task.FromResult(new List<UnitTest>());
    }
}