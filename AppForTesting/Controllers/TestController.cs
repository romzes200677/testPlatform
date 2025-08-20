using CSharpTestApp.Models;
using CSharpTestApp.Models.api;
using Microsoft.AspNetCore.Mvc;
using CSharpTestApp.Services;

namespace CSharpTestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ICodeExecutionService _codeExecutionService;


        public TestController(IQuestionService questionService, ICodeExecutionService codeExecutionService)
        {
            _questionService = questionService;
            _codeExecutionService = codeExecutionService;
        }

        [HttpGet("questions")]
        public ActionResult<PagedResponse<Question>> GetQuestions([FromQuery] PagingParameters parameters)
        {
            var questions = _questionService.GenerateTest();
            var totalCount = questions.Count;
            var paginatedQuestions = questions
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();

            var response = new PagedResponse<Question>
            {
                Items = paginatedQuestions,
                Count = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)parameters.PageSize)
            };
            return Ok(response);
        }
        
        [HttpGet("decisions")]
        public IActionResult GetDecisions(int questionId)
        {
            var questions = _questionService.GenerateArthurTests();
            return Ok(questions);
        }
        
        [HttpPost("evaluate")]
        public IActionResult EvaluateTest([FromBody] List<UserAnswer> userAnswers)
        {
            var questions = _questionService.GenerateTest();
            var result = _questionService.EvaluateTest(userAnswers, questions);
            return Ok(result);
        }
        
        [HttpPost("execute")]
        public async Task<ActionResult<CodeExecutionResponse>> ExecuteCode([FromBody] CodeExecutionRequest request)
        {
            var result = await _codeExecutionService.ExecuteCodeAsync(request);
            return Ok(result);
        }
    }
}