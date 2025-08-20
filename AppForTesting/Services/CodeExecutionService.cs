using CSharpTestApp.Controllers;
using CSharpTestApp.Models;
using CSharpTestApp.Models.api;

namespace CSharpTestApp.Services
{
    public interface ICodeExecutionService
    {
        Task<CodeExecutionResponse> ExecuteCodeAsync(CodeExecutionRequest request);
    }

    public class CodeExecutionService : ICodeExecutionService
    {
        private readonly IQuestionService _questionService;
        private readonly ICodeTester _codeTester;

        public CodeExecutionService(IQuestionService questionService, ICodeTester codeTester)
        {
            _questionService = questionService;
            _codeTester = codeTester;
        }

        public async Task<CodeExecutionResponse> ExecuteCodeAsync(CodeExecutionRequest request)
        {
            List<UnitTest> tests = new List<UnitTest>();
            if (request.AssignmentId == "math-1")
            {
                tests = _questionService.GenerateArthurTests();
            }
            else if (request.AssignmentId == "math-2")
            {
                tests = await _questionService.GetTests();
            }

            string fullCode = request.MainMethodTemplate.Replace("###", request.UserCode);

            var testResult = _codeTester.RunTests(fullCode, tests);
            var incorrectModels = testResult.FailedTests.OrderBy(x => x.Inputs).FirstOrDefault();
            var lstErrors = incorrectModels is null ? new List<TestResultDto>() : new List<TestResultDto>()
            {
                new()
                {
                    TestName = incorrectModels.Name,
                    Inputs = incorrectModels.Inputs, // Используем список строк
                    Expected = incorrectModels.ExpectedOutput,
                    Actual = incorrectModels.ActualOutput
                }
            };
            return new CodeExecutionResponse
            {
                IsSuccess = testResult.IsSuccess,
                FailedTests = lstErrors,
                CompilationError = testResult.CompilationError,
                Output = testResult.Output,
                ExecutionTime = testResult.ExecutionTime.TotalMilliseconds
            };
        }
    }
}