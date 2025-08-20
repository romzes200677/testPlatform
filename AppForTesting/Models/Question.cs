namespace CSharpTestApp.Models;

public class Question
{
    public int Id { get; set; }
    public string Topic { get; set; }
    public string Text { get; set; }
    public List<AnswerOption> Options { get; set; }
    public List<int> CorrectAnswers { get; set; } // Изменено для поддержки нескольких правильных ответов
    public string Explanation { get; set; }
    public int Complexity { get; set; } // Добавлено поле сложности
    public string QuestionType { get; set; } // Добавлено поле типа вопроса
}

public class AnswerOption
{
    public int Id { get; set; }
    public string Text { get; set; }
}

public class UserAnswer
{
    public int QuestionId { get; set; }
    public List<int> SelectedOptionIds { get; set; } // Изменено для поддержки нескольких выборов
}

public class TestResult
{
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public List<QuestionResult> IncorrectAnswers { get; set; }
}

public class QuestionResult
{
    public Question Question { get; set; }
    public List<int> SelectedAnswerIds { get; set; }
}