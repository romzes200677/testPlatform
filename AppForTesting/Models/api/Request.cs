namespace CSharpTestApp.Models.api;

// Модель запроса от React
public class ApiRequest
{
    public string AssignmentText { get; set; }  // Текст задания (Markdown)
    public string MainMethodTemplate { get; set; } // Шаблон с ###
    public string UserCode { get; set; }  // Код пользователя для вставки
    public string AssignmentId { get; set; } // Идентификатор задания
}