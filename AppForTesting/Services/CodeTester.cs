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
            var compilationResult = CompileCode(sourceCode);
            if (!compilationResult.Success)
            {
                result.CompilationError = compilationResult.ErrorMessage;
                return result;
            }

            using (var sandbox = new CodeExecutionSandbox())
            {
                var sandboxResult = sandbox.ExecuteTests(
                    compilationResult.AssemblyBytes, 
                    tests);
                
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
        var parseOptions = CSharpParseOptions.Default
            .WithLanguageVersion(LanguageVersion.CSharp12);
        
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode, parseOptions);
        string assemblyName = Path.GetRandomFileName();

        List<MetadataReference> references = GetSystemReferences();
        references.Add(MetadataReference.CreateFromFile(Assembly.GetEntryAssembly().Location));

        var compilationOptions = new CSharpCompilationOptions(
            OutputKind.ConsoleApplication, // Изменено на консольное приложение
            optimizationLevel: OptimizationLevel.Release,
            platform: Platform.AnyCpu
        ).WithSpecificDiagnosticOptions(new Dictionary<string, ReportDiagnostic>
        {
            { "CS8019", ReportDiagnostic.Suppress }
        });

        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: compilationOptions
        );

        using var ms = new MemoryStream();
        EmitResult result = compilation.Emit(ms);

        if (!result.Success)
        {
            var errors = result.Diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Select(d => $"{d.Id}: {d.GetMessage()}")
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
            AssemblyBytes = ms.ToArray()
        };
    }

    private List<MetadataReference> GetSystemReferences()
    {
        var references = new List<MetadataReference>();
        
        string? trustedAssemblies = AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") as string;
        
        if (!string.IsNullOrEmpty(trustedAssemblies))
        {
            char separator = Path.PathSeparator;
            foreach (var path in trustedAssemblies.Split(separator))
            {
                if (File.Exists(path))
                {
                    references.Add(MetadataReference.CreateFromFile(path));
                }
            }
        }
        else
        {
            // Fallback для .NET Framework или старых версий
            references.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
            references.Add(MetadataReference.CreateFromFile(typeof(Console).Assembly.Location));
            references.Add(MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location));
            references.Add(MetadataReference.CreateFromFile(typeof(System.Runtime.GCSettings).Assembly.Location));
        }

        // Добавляем обязательные сборки для .NET 8
        references.Add(MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.Unsafe).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Diagnostics.Debug).Assembly.Location));
        
        return references;
    }
}

public class CompilationResult
{
    public bool Success { get; set; }
    public byte[] AssemblyBytes { get; set; }
    public string ErrorMessage { get; set; }
}

public class TestRunResult
{
    public bool IsSuccess { get; set; }
    public List<UnitTest> FailedTests { get; set; } = new();
    public string CompilationError { get; set; }
    public string Output { get; set; }
    public TimeSpan ExecutionTime { get; set; }
}


