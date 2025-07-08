namespace CSharpTestApp.Models;

public class UnitTest
{
    public string Name { get; set; }          // Название теста
    public string MethodName { get; set; }    // Имя метода для тестирования
    public object[] Parameters { get; set; }  // Параметры для передачи в метод
    public object ExpectedResult { get; set; }// Ожидаемый результат
    public string ActualResult { get; set; }  // Фактический результат (для отчетов)
    public string ErrorMessage { get; set; }  // Сообщение об ошибке (если было исключение)
    public bool IsSuccess { get; set; }       // Прошел ли тест
    public List<string> Inputs { get; set; } = new();      // Новое свойство
    public string ExpectedOutput { get; set; } // Новое свойство
    public string ActualOutput { get; set; }     // Фактический вывод

    // Проверяет, соответствует ли фактический результат ожидаемому
    public bool IsPassing(object actual)
    {
        ActualResult = actual?.ToString();
        
        if (actual == null && ExpectedResult == null) 
            return true;
        if (actual == null || ExpectedResult == null) 
            return false;
        
        return actual.Equals(ExpectedResult);
    }
}