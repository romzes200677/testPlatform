using CSharpTestApp.Infrastructure;
using CSharpTestApp.Models;
using Microsoft.AspNetCore.Mvc;
using CSharpTestApp.Services;

namespace CSharpTestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly TestService _testService;
        private readonly ICodeTester _codeTester;
        private readonly ITestRepository _testRepository;

        public TestController(TestService testService, ICodeTester codeTester, ITestRepository testRepository)
        {
            _testService = testService;
            _codeTester = codeTester;
            _testRepository = testRepository;
        }

        [HttpGet("questions")]
        public IActionResult GetQuestions()
        {
            var questions = _testService.GenerateTest();
            return Ok(questions);
        }
        
        [HttpGet("decisions")]
        public IActionResult GetDecisions(int questionId)
        {
            var questions = _testService.GenerateArthurTests();
            return Ok(questions);
        }
        
        [HttpPost("evaluate")]
        public IActionResult EvaluateTest([FromBody] List<UserAnswer> userAnswers)
        {
            var questions = _testService.GenerateTest();
            var result = _testService.EvaluateTest(userAnswers, questions);
            return Ok(result);
        }
        
        [HttpPost("execute")]
        public async Task<ActionResult<CodeExecutionResponse>> ExecuteCode([FromBody] CodeExecutionRequest request)
        {
            List<UnitTest> tests = new List<UnitTest>();
            if (request.AssignmentId == "math-1")
            {
                tests = _testService.GenerateArthurTests();
            }else if (request.AssignmentId == "math-2")
            {
                tests = await _testService.GetTests();
            }
            
            string fullCode = request.MainMethodTemplate.Replace("###", request.UserCode);

            var testResult = _codeTester.RunTests(fullCode, tests);
            var incorrectModels = testResult.FailedTests.OrderBy(x=>x.Inputs).FirstOrDefault();
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
    
    public class TestResultDto
    {
        public string TestName { get; set; }
        public List<string> Inputs { get; set; } // Изменено на список строк
        public string Expected { get; set; }
        public string Actual { get; set; }
    }

    public class CodeExecutionResponse
    {
        public bool IsSuccess { get; set; }
        public List<TestResultDto> FailedTests { get; set; } = new();
        public string CompilationError { get; set; }
        public string Output { get; set; }
        public double ExecutionTime { get; set; }
    }
    
    public class CodeExecutionRequest
    {
        /// <summary>
        /// Идентификатор задания (для получения соответствующих тестов)
        /// </summary>
        public string AssignmentId { get; set; }

        /// <summary>
        /// Текст задания с поддержкой Markdown
        /// </summary>
        public string AssignmentText { get; set; }

        /// <summary>
        /// Шаблон метода Main с плейсхолдером ###
        /// </summary>
        public string MainMethodTemplate { get; set; }

        /// <summary>
        /// Код пользователя для вставки в плейсхолдер ###
        /// </summary>
        public string UserCode { get; set; }
    }
}