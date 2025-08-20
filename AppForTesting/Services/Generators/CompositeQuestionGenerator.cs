using CSharpTestApp.Models;

namespace CSharpTestApp.Services.Generators
{
    public class CompositeQuestionGenerator : IQuestionGenerator
    {
        private readonly IEnumerable<IQuestionGenerator> _generators;

        private readonly Func<IEnumerable<IQuestionGenerator>> _generatorsFactory;

        public CompositeQuestionGenerator(Func<IEnumerable<IQuestionGenerator>> generatorsFactory)
        {
            _generatorsFactory = generatorsFactory;
        }

        public IEnumerable<Question> GenerateQuestions()
        {
            foreach (var generator in _generatorsFactory())
            {
                foreach (var question in generator.GenerateQuestions())
                {
                    yield return question;
                }
            }
        }
    }
}