namespace CSharpTestApp.Models;

public class Question
{
    public int Id { get; set; }
    public string Topic { get; set; }
    public string Text { get; set; }
    public List<AnswerOption> Options { get; set; }
    public int CorrectAnswerId { get; set; }
    public string Explanation { get; set; }
}

public class AnswerOption
{
    public int Id { get; set; }
    public string Text { get; set; }
}

public class UserAnswer
{
    public int QuestionId { get; set; }
    public int SelectedAnswerId { get; set; }
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
    public int SelectedAnswerId { get; set; }
}