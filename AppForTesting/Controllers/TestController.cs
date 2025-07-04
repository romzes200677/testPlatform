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
        
        public TestController(TestService testService)
        {
            _testService = testService;
        }
        
        [HttpGet("questions")]
        public IActionResult GetQuestions()
        {
            var questions = _testService.GenerateTest();
            return Ok(questions);
        }
        
        [HttpPost("evaluate")]
        public IActionResult EvaluateTest([FromBody] List<UserAnswer> userAnswers)
        {
            var questions = _testService.GenerateTest();
            var result = _testService.EvaluateTest(userAnswers, questions);
            return Ok(result);
        }
    }
}