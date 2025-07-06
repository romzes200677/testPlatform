using System.Diagnostics;
using System.Reflection;
using CSharpTestApp.Models;
using CSharpTestApp.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

public interface ICodeTester
{
    TestRunResult RunTests(string sourceCode, List<UnitTest> tests);
}

public class CodeTester : ICodeTester
{
    public TestRunResult RunTests(string sourceCode, List<UnitTest> tests)
    {
        var result = new TestRunResult();
        var stopwatch = Stopwatch.StartNew();
    
        try
        {
            // Компиляция кода
            var compilationResult = CompileCode(sourceCode);
            if (!compilationResult.Success)
            {
                result.CompilationError = compilationResult.ErrorMessage;
                return result;
            }

            // Используем песочницу для выполнения
            using (var sandbox = new CodeExecutionSandbox())
            {
                var sandboxResult = sandbox.ExecuteTests(
                    compilationResult.AssemblyBytes, 
                    tests);
            
                // Копируем результаты из песочницы
                result.IsSuccess = sandboxResult.IsSuccess;
                result.FailedTests = sandboxResult.FailedTests;
                result.CompilationError = sandboxResult.CompilationError;
                result.Output = sandboxResult.Output;
            }
        }
        catch (Exception ex)
        {
            result.CompilationError = $"Unexpected error: {ex.Message}";
        }
        finally
        {
            stopwatch.Stop();
            result.ExecutionTime = stopwatch.Elapsed;
        }
    
        return result;
    }

    private CompilationResult CompileCode(string sourceCode)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
    
        string assemblyName = Path.GetRandomFileName();
        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
            MetadataReference.CreateFromFile(Assembly.GetEntryAssembly().Location)
        };

        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using (var ms = new MemoryStream())
        {
            EmitResult result = compilation.Emit(ms);
        
            if (!result.Success)
            {
                var errors = result.Diagnostics
                    .Where(d => d.IsWarningAsError || d.Severity == DiagnosticSeverity.Error)
                    .Select(d => d.GetMessage())
                    .ToList();
            
                return new CompilationResult
                {
                    Success = false,
                    ErrorMessage = string.Join("\n", errors)
                };
            }
        
            ms.Seek(0, SeekOrigin.Begin);
            return new CompilationResult
            {
                Success = true,
                AssemblyBytes = ms.ToArray() // Сохраняем байты сборки
            };
        }
    }
}

// Вспомогательный класс для результатов компиляции
public class CompilationResult
{
    public bool Success { get; set; }
    public byte[] AssemblyBytes { get; set; } // Теперь здесь хранятся байты сборки
    public string ErrorMessage { get; set; }
}

// Результат выполнения тестов
public class TestRunResult
{
    public bool IsSuccess { get; set; }               // Общий результат
    public List<UnitTest> FailedTests { get; set; }   // Непройденные тесты
    public string CompilationError { get; set; }      // Ошибки компиляции
    public string Output { get; set; }                // Вывод программы
    public TimeSpan ExecutionTime { get; set; }       // Время выполнения

    public TestRunResult()
    {
        FailedTests = new List<UnitTest>();
    }
}