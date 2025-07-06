namespace CSharpTestApp.Models.api;

// Модель ответа
public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public List<ApiResult> FailedTests { get; set; } = new();
    public string CompilationError { get; set; }
    public string Output { get; set; }
}