using CSharpTestApp.Models;

namespace CSharpTestApp.Services.Generators;

public class CompositeGenerator : IQuestionGenerator
{
    private readonly IEnumerable<IQuestionGenerator> _generators;

    public CompositeGenerator(IEnumerable<IQuestionGenerator> generators)
    {
        _generators = generators;
    }

    public IEnumerable<Question> GenerateQuestions()
    {
        foreach (var generator in _generators)
        {
            foreach (var question in generator.GenerateQuestions())
            {
                yield return question;
            }
        }
    }
}