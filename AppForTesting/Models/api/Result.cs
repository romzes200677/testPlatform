namespace CSharpTestApp.Models.api;

public class ApiResult
{
    public string TestName { get; set; }
    public string Expected { get; set; }
    public string Actual { get; set; }
    public string ErrorMessage { get; set; }
}