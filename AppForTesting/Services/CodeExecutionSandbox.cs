using System.Reflection;
using System.Runtime.Loader;
using CSharpTestApp.Models;

namespace CSharpTestApp.Services;

public class CodeExecutionSandbox : IDisposable
{
    private readonly AssemblyLoadContext _loadContext;

    public CodeExecutionSandbox()
    {
        _loadContext = new AssemblyLoadContext(
            name: "CodeSandbox_" + Guid.NewGuid(), 
            isCollectible: true);
    }

    public TestRunResult ExecuteTests(byte[] assemblyBytes, List<UnitTest> tests)
    {
        try
        {
            using var stream = new MemoryStream(assemblyBytes);
            var assembly = _loadContext.LoadFromStream(stream);
            
            var executor = new RemoteExecutor();
            return executor.ExecuteTestsInIsolation(assembly, tests);
        }
        catch (Exception ex)
        {
            return new TestRunResult
            {
                IsSuccess = false,
                CompilationError = $"Sandbox error: {ex.Message}"
            };
        }
    }

    public void Dispose() => _loadContext.Unload();

    public class RemoteExecutor
    {
        public TestRunResult ExecuteTestsInIsolation(Assembly assembly, List<UnitTest> tests)
        {
            var result = new TestRunResult();
            var originalOut = Console.Out;
            var originalIn = Console.In;

            try
            {
                var mainMethod = assembly.EntryPoint;
                if (mainMethod == null)
                {
                    result.CompilationError = "Entry point not found";
                    return result;
                }

                foreach (var test in tests)
                {
                    try
                    {
                        string combinedInput = string.Join(Environment.NewLine, test.Inputs);
                        using (var inputReader = new StringReader(combinedInput))
                        using (var outputWriter = new StringWriter())
                        {
                            Console.SetIn(inputReader);
                            Console.SetOut(outputWriter);

                            object[] parameters = mainMethod.GetParameters().Length > 0 
                                ? new object[] { Array.Empty<string>() } 
                                : null;
                            
                            mainMethod.Invoke(null, parameters);
                            
                            var actualOutput = outputWriter.ToString().Trim();
                            test.ActualOutput = actualOutput;

                            if (actualOutput != test.ExpectedOutput.Trim())
                            {
                                result.FailedTests.Add(test);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        test.ActualOutput = $"Runtime error: {ex.InnerException?.Message ?? ex.Message}";
                        result.FailedTests.Add(test);
                    }
                    finally
                    {
                        Console.SetOut(originalOut);
                        Console.SetIn(originalIn);
                    }
                }

                result.IsSuccess = !result.FailedTests.Any();
            }
            catch (Exception ex)
            {
                result.CompilationError = $"Execution failed: {ex.Message}";
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.SetIn(originalIn);
            }
            
            return result;
        }
    }
}