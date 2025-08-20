using CSharpTestApp.Models.api;

namespace CSharpTestApp.Models
{
    public class CodeExecutionResponse
    {
        public bool IsSuccess { get; set; }
        public List<TestResultDto> FailedTests { get; set; } = new();
        public string CompilationError { get; set; }
        public string Output { get; set; }
        public double ExecutionTime { get; set; }
    }
}