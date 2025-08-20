using CSharpTestApp.Models;
using CSharpTestApp.Services.Generators;

namespace CSharpTestApp.Services
{

    public interface IQuestionService
    {
        List<Question> GenerateTest();
        TestResult EvaluateTest(List<UserAnswer> userAnswers, List<Question> questions);
        List<UnitTest> GenerateArthurTests();
        Task<List<UnitTest>> GetTests();
    }
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionGenerator _questionGenerator;
        
        public QuestionService(IQuestionGenerator questionGenerator)
        {
            _questionGenerator = questionGenerator;

        }

        public List<Question> GenerateTest()
        {
            var questions = new List<Question>();
          
                questions.AddRange(_questionGenerator.GenerateQuestions());
            return questions;
        }
    
        public TestResult EvaluateTest(List<UserAnswer> userAnswers, List<Question> questions)
        {
            var correctAnswers = 0;
            var incorrectAnswers = new List<QuestionResult>();

            foreach (var userAnswer in userAnswers)
            {
                var question = questions.FirstOrDefault(q => q.Id == userAnswer.QuestionId);
                if (question != null)
                {
                    if (question.CorrectAnswers.OrderBy(x => x).SequenceEqual(userAnswer.SelectedOptionIds.OrderBy(x => x)))
                    {
                        correctAnswers++;
                    }
                    else
                    {
                        incorrectAnswers.Add(new QuestionResult { Question = question, SelectedAnswerIds = userAnswer.SelectedOptionIds });
                    }
                }
            }

            return new TestResult
            {
                TotalQuestions = questions.Count,
                CorrectAnswers = correctAnswers,
                IncorrectAnswers = incorrectAnswers
            };
        }

        public List<UnitTest> GenerateArthurTests()
        {
                return new List<UnitTest>
                {
                    new UnitTest
                    {
                        Name = "Sample Input 1",
                        Inputs = new List<string> { "10","10","100" }, // Используем список
                        ExpectedOutput = "40"
                    },
                    new UnitTest
                    {
                        Name = "Sample Input 2",
                        Inputs = new List<string> { "10","10","5" }, // Используем список
                        ExpectedOutput = "25"
                    },
                    new UnitTest
                    {
                        Name = "Sample Input 3",
                        Inputs = new List<string> { "5","100","1" }, // Используем список
                        ExpectedOutput = "12"
                    },
                    new UnitTest
                    {
                        Name = "Sample Input 4",
                        Inputs = new List<string> { "100", "5", "1" },
                        ExpectedOutput = "12"
                    }

                };
        }

        public async Task<List<UnitTest>> GetTests()
        {
            return await Task.FromResult(new List<UnitTest>
            {
                // Тесты для калькулятора
                new UnitTest
                {
                    Name = "Addition of positive numbers",
                    MethodName = "Add",
                    Parameters = new object[] { 5, 3 },
                    ExpectedResult = 8
                }
            });
        }
    
 
    }
}