using CSharpTestApp.Models;

namespace CSharpTestApp.Services
{
    public class TestService
    {
        public List<Question> GenerateTest()
        {
            // Реализация генерации вопросов (50 вопросов)
           return new List<Question>
            {
                new Question
                {
                    Id = 1,
                    Topic = "Ввод-вывод данных",
                    Text = "Какой метод используется для вывода текста в консоль в C#?",
                    Options = new List<AnswerOption>
                    {
                        new AnswerOption { Id = 1, Text = "Console.Read()" },
                        new AnswerOption { Id = 2, Text = "Console.WriteLine()" },
                        new AnswerOption { Id = 3, Text = "System.Print()" },
                        new AnswerOption { Id = 4, Text = "Output.Write()" }
                    },
                    CorrectAnswerId = 2,
                    Explanation = "Метод Console.WriteLine() выводит текст в консоль и добавляет новую строку."
                },
                new Question
                {
                    Id = 2,
                    Topic = "Переменные",
                    Text = "Какой тип данных лучше подходит для хранения денежных сумм в C#?",
                    Options = new List<AnswerOption>
                    {
                        new AnswerOption { Id = 1, Text = "int" },
                        new AnswerOption { Id = 2, Text = "float" },
                        new AnswerOption { Id = 3, Text = "decimal" },
                        new AnswerOption { Id = 4, Text = "double" }
                    },
                    CorrectAnswerId = 3,
                    Explanation = "Тип decimal обеспечивает высокую точность для финансовых расчетов."
                },
                new Question
                {
                    Id = 3,
                    Topic = "Условные операторы",
                    Text = "Какой оператор используется для множественного выбора в C#?",
                    Options = new List<AnswerOption>
                    {
                        new AnswerOption { Id = 1, Text = "if-else" },
                        new AnswerOption { Id = 2, Text = "switch-case" },
                        new AnswerOption { Id = 3, Text = "for" },
                        new AnswerOption { Id = 4, Text = "while" }
                    },
                    CorrectAnswerId = 2,
                    Explanation = "Оператор switch-case предназначен для обработки множественных условий."
                },
                new Question
                {
                    Id = 4,
                    Topic = "Циклы",
                    Text = "Сколько итераций выполнит цикл: for (int i = 5; i > 0; i--)?",
                    Options = new List<AnswerOption>
                    {
                        new AnswerOption { Id = 1, Text = "4" },
                        new AnswerOption { Id = 2, Text = "5" },
                        new AnswerOption { Id = 3, Text = "6" },
                        new AnswerOption { Id = 4, Text = "Бесконечно" }
                    },
                    CorrectAnswerId = 2,
                    Explanation = "Цикл выполняется при i=5,4,3,2,1 - всего 5 итераций."
                },
                new Question
                {
                    Id = 5,
                    Topic = "Функции",
                    Text = "Как объявить функцию, которая возвращает сумму двух чисел?",
                    Options = new List<AnswerOption>
                    {
                        new AnswerOption { Id = 1, Text = "void Sum(int a, int b)" },
                        new AnswerOption { Id = 2, Text = "int Sum(int a, int b)" },
                        new AnswerOption { Id = 3, Text = "function Sum(a, b)" },
                        new AnswerOption { Id = 4, Text = "def Sum(a, b)" }
                    },
                    CorrectAnswerId = 2,
                    Explanation = "Правильный синтаксис: <возвращаемый_тип> <имя_функции>(<параметры>)"
                },
                // Добавьте остальные 45 вопросов по аналогии
                // Для тестирования можно временно использовать меньше вопросов
                new Question
                {
                    Id = 6,
                    Topic = "Массивы",
                    Text = "Как создать массив из 5 целых чисел?",
                    Options = new List<AnswerOption>
                    {
                        new AnswerOption { Id = 1, Text = "int[] arr = new int[5];" },
                        new AnswerOption { Id = 2, Text = "array arr = [5];" },
                        new AnswerOption { Id = 3, Text = "int arr = array(5);" },
                        new AnswerOption { Id = 4, Text = "List<int> arr = new(5);" }
                    },
                    CorrectAnswerId = 1
                }
            };
        }

        public TestResult EvaluateTest(List<UserAnswer> userAnswers, List<Question> questions)
        {
            int correctCount = 0;
            var incorrectAnswers = new List<QuestionResult>();
            
            // Создаем словарь для быстрого поиска вопросов по ID
            var questionDict = questions.ToDictionary(q => q.Id);
            
            foreach (var userAnswer in userAnswers)
            {
                if (questionDict.TryGetValue(userAnswer.QuestionId, out var question))
                {
                    if (userAnswer.SelectedAnswerId == question.CorrectAnswerId)
                    {
                        correctCount++;
                    }
                    else
                    {
                        incorrectAnswers.Add(new QuestionResult
                        {
                            Question = question,
                            SelectedAnswerId = userAnswer.SelectedAnswerId
                        });
                    }
                }
            }
            
            return new TestResult
            {
                TotalQuestions = questions.Count, // Общее количество вопросов
                CorrectAnswers = correctCount,    // Количество правильных ответов
                IncorrectAnswers = incorrectAnswers
            };
        }
    }
}