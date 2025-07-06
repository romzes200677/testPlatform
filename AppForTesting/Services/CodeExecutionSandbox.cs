using CSharpTestApp.Models;
using System.Reflection;
using System.Runtime.Loader;

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
            return executor.ExecuteTestsInIsolation(assembly, tests); // Передаем Assembly вместо byte[]
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
        public TestRunResult ExecuteTestsInIsolation(Assembly assembly, List<UnitTest> tests) // Измененный параметр
        {
            var result = new TestRunResult();
            var outputCapture = new StringWriter();

            try
            {
                // Перенаправляем консольный вывод
                var originalOut = Console.Out;
                Console.SetOut(outputCapture);

                // Находим и запускаем метод Main
                var mainMethod = assembly.EntryPoint;
                if (mainMethod != null)
                {
                    object[] parameters = mainMethod.GetParameters().Length > 0 
                        ? new object[] { Array.Empty<string>() } 
                        : null;
                    
                    mainMethod.Invoke(null, parameters);
                }

                // Выполняем тесты
                ExecuteTests(assembly, tests, result);
                
                // Восстанавливаем вывод
                Console.SetOut(originalOut);
                result.Output = outputCapture.ToString();
            }
            catch (Exception ex)
            {
                result.CompilationError = $"Runtime error: {ex.InnerException?.Message ?? ex.Message}";
            }
            
            return result;
        }

        private void ExecuteTests(Assembly assembly, List<UnitTest> tests, TestRunResult result)
        {
            foreach (var test in tests)
            {
                try
                {
                    // Находим тестируемый класс и метод
                    var targetType = assembly.GetTypes()
                        .FirstOrDefault(t => t.GetMethod(test.MethodName) != null);
                    
                    if (targetType == null)
                    {
                        test.ErrorMessage = $"Method {test.MethodName} not found";
                        result.FailedTests.Add(test);
                        continue;
                    }
                    
                    // Создаем экземпляр и вызываем метод
                    var instance = Activator.CreateInstance(targetType);
                    var method = targetType.GetMethod(test.MethodName);
                    var actualResult = method.Invoke(instance, test.Parameters);
                    
                    // Проверяем результат
                    test.IsSuccess = test.IsPassing(actualResult);
                    if (!test.IsSuccess)
                    {
                        test.ActualResult = actualResult?.ToString();
                        result.FailedTests.Add(test);
                    }
                }
                catch (Exception ex)
                {
                    test.ErrorMessage = ex.InnerException?.Message ?? ex.Message;
                    result.FailedTests.Add(test);
                }
            }
        }
    }
}