namespace CSharpTestApp.Models.api
{
    public class TestResultDto
    {
        public string TestName { get; set; }
        public List<string> Inputs { get; set; } // Изменено на список строк
        public string Expected { get; set; }
        public string Actual { get; set; }
    }
}