# Архитектура проекта CSharpTestApp

## Диаграмма компонентов

```mermaid
graph TD
    subgraph "Frontend (React)"
        Client["Client App (React)"] --> CodeEditor["CodeEditor Component"] 
        Client --> TestPage["TestPage Component"]
        Client --> ResultsPage["ResultsPage Component"]
        Client --> CodeExecutionPage["CodeExecutionPage Component"]
    end

    subgraph "Backend (ASP.NET Core)"
        API["ASP.NET Core API"] --> TestController["TestController"]
        TestController --> TestService["TestService"]
        TestController --> CodeTester["CodeTester"]
        TestController --> TestRepository["TestRepository"]
        CodeTester --> CodeExecutionSandbox["CodeExecutionSandbox"]
    end

    subgraph "Models"
        Question["Question"]
        UnitTest["UnitTest"]
        TestResult["TestResult"]
        CompilationResult["CompilationResult"]
    end

    CodeExecutionPage -- "HTTP Requests" --> API
    TestPage -- "HTTP Requests" --> API
```

## Диаграмма классов

```mermaid
classDiagram
    class TestController {
        -TestService _testService
        -ICodeTester _codeTester
        -ITestRepository _testRepository
        +GetQuestions()
        +GetDecisions()
        +EvaluateTest()
        +ExecuteCode()
    }

    class TestService {
        +GenerateTest()
        +EvaluateTest()
        +GenerateArthurTests()
    }

    class ICodeTester {
        <<interface>>
        +RunTests()
    }

    class CodeTester {
        +RunTests()
        -CompileCode()
    }

    class CodeExecutionSandbox {
        -AssemblyLoadContext _loadContext
        +ExecuteTests()
        +Dispose()
    }

    class ITestRepository {
        <<interface>>
        +GetTestsForAssignment()
    }

    class InMemoryTestRepository {
        -Dictionary _assignments
        +GetTestsForAssignment()
    }

    class UnitTest {
        +string Name
        +string MethodName
        +object[] Parameters
        +object ExpectedResult
        +string ActualResult
        +string ErrorMessage
        +bool IsSuccess
        +List~string~ Inputs
        +string ExpectedOutput
        +string ActualOutput
        +bool IsPassing()
    }

    class Question {
        +int Id
        +string Topic
        +string Text
        +List~AnswerOption~ Options
        +int CorrectAnswerId
        +string Explanation
    }

    class TestRunResult {
        +bool IsSuccess
        +List~UnitTest~ FailedTests
        +string CompilationError
        +string Output
        +TimeSpan ExecutionTime
    }

    TestController --> TestService
    TestController --> ICodeTester
    TestController --> ITestRepository
    ICodeTester <|.. CodeTester
    CodeTester --> CodeExecutionSandbox
    ITestRepository <|.. InMemoryTestRepository
```

## Диаграмма последовательности выполнения кода

```mermaid
sequenceDiagram
    participant Client as Frontend Client
    participant API as TestController
    participant Tester as CodeTester
    participant Sandbox as CodeExecutionSandbox
    participant Repo as TestRepository

    Client->>API: POST /api/test/execute (код + assignmentId)
    API->>Repo: GetTestsForAssignment(assignmentId)
    Repo-->>API: List<UnitTest>
    API->>Tester: RunTests(sourceCode, tests)
    Tester->>Tester: CompileCode(sourceCode)
    Tester->>Sandbox: ExecuteTests(assembly, tests)
    Sandbox->>Sandbox: LoadAssembly
    Sandbox->>Sandbox: ExecuteTestsInIsolation
    Sandbox-->>Tester: TestRunResult
    Tester-->>API: TestRunResult
    API-->>Client: CodeExecutionResponse
```

## Архитектурные особенности

1. **Клиент-серверная архитектура**:
   - Frontend: React приложение с компонентами для тестирования и выполнения кода
   - Backend: ASP.NET Core API с контроллерами, сервисами и репозиториями

2. **Слои приложения**:
   - Presentation Layer: React компоненты и ASP.NET Core контроллеры
   - Business Logic Layer: Сервисы (TestService, CodeTester)
   - Data Access Layer: Репозитории (InMemoryTestRepository)
   - Domain Model: Модели данных (Question, UnitTest, TestResult)

3. **Ключевые компоненты**:
   - **CodeTester**: Компиляция и выполнение C# кода
   - **CodeExecutionSandbox**: Изолированное выполнение скомпилированного кода
   - **TestService**: Генерация и оценка тестов
   - **TestRepository**: Хранение и получение тестовых данных

4. **Паттерны проектирования**:
   - Repository Pattern: ITestRepository и InMemoryTestRepository
   - Dependency Injection: Внедрение зависимостей через конструкторы
   - Strategy Pattern: Интерфейс ICodeTester и его реализация