# Архитектура проекта CSharpTestApp (PlantUML)

## Компонентная диаграмма

```plantuml
@startuml
package "Frontend (React)" {
  [Client App] as Client
  [CodeEditor] as CodeEditor
  [TestPage] as TestPage
  [ResultsPage] as ResultsPage
  [CodeExecutionPage] as CodeExecPage
  
  Client --> CodeEditor
  Client --> TestPage
  Client --> ResultsPage
  Client --> CodeExecPage
}

package "Backend (ASP.NET Core)" {
  [ASP.NET Core API] as API
  [TestController] as TestController
  [TestService] as TestService
  [CodeTester] as CodeTester
  [CodeExecutionSandbox] as Sandbox
  [TestRepository] as Repository
  
  API --> TestController
  TestController --> TestService
  TestController --> CodeTester
  TestController --> Repository
  CodeTester --> Sandbox
}

package "Models" {
  [Question] as Question
  [UnitTest] as UnitTest
  [TestResult] as TestResult
  [CompilationResult] as CompilationResult
}

CodeExecPage -right-> API : HTTP Requests
TestPage -right-> API : HTTP Requests
@enduml
```

## Диаграмма классов

```plantuml
@startuml
interface ICodeTester {
  +RunTests(string sourceCode, List<UnitTest> tests): TestRunResult
}

class CodeTester {
  +RunTests(string sourceCode, List<UnitTest> tests): TestRunResult
  -CompileCode(string sourceCode): CompilationResult
}

class CodeExecutionSandbox {
  -AssemblyLoadContext _loadContext
  +ExecuteTests(byte[] assemblyBytes, List<UnitTest> tests): TestRunResult
  +Dispose(): void
}

class RemoteExecutor {
  +ExecuteTestsInIsolation(Assembly assembly, List<UnitTest> tests): TestRunResult
}

interface ITestRepository {
  +GetTestsForAssignment(string assignmentId): Task<List<UnitTest>>
}

class InMemoryTestRepository {
  -Dictionary<string, List<UnitTest>> _assignments
  +GetTestsForAssignment(string assignmentId): Task<List<UnitTest>>
}

class TestController {
  -TestService _testService
  -ICodeTester _codeTester
  -ITestRepository _testRepository
  +GetQuestions(PagingParameters parameters): ActionResult<PagedResponse<Question>>
  +GetDecisions(int questionId): IActionResult
  +EvaluateTest(List<UserAnswer> userAnswers): IActionResult
  +ExecuteCode(CodeExecutionRequest request): Task<ActionResult<CodeExecutionResponse>>
}

class TestService {
  +GenerateTest(): List<Question>
  +EvaluateTest(List<UserAnswer> userAnswers, List<Question> questions): TestResult
  +GenerateArthurTests(): List<UnitTest>
  +GetTests(): Task<List<UnitTest>>
}

class UnitTest {
  +string Name
  +string MethodName
  +object[] Parameters
  +object ExpectedResult
  +string ActualResult
  +string ErrorMessage
  +bool IsSuccess
  +List<string> Inputs
  +string ExpectedOutput
  +string ActualOutput
  +bool IsPassing(object actual): bool
}

class TestRunResult {
  +bool IsSuccess
  +List<UnitTest> FailedTests
  +string CompilationError
  +string Output
  +TimeSpan ExecutionTime
}

class CompilationResult {
  +bool Success
  +string ErrorMessage
  +byte[] AssemblyBytes
}

ICodeTester <|.. CodeTester
CodeTester --> CodeExecutionSandbox
CodeExecutionSandbox --> RemoteExecutor
ITestRepository <|.. InMemoryTestRepository
TestController --> TestService
TestController --> ICodeTester
TestController --> ITestRepository
CodeTester ..> CompilationResult
CodeTester ..> TestRunResult
CodeExecutionSandbox ..> TestRunResult
@enduml
```

## Диаграмма последовательности

```plantuml
@startuml
actor User
participant "React Frontend" as Frontend
participant "TestController" as Controller
participant "TestService" as Service
participant "CodeTester" as Tester
participant "CodeExecutionSandbox" as Sandbox
participant "TestRepository" as Repository

User -> Frontend: Вводит код и выбирает задание
Frontend -> Controller: POST /api/test/execute
Controller -> Repository: GetTestsForAssignment(assignmentId)
Repository --> Controller: List<UnitTest>
Controller -> Tester: RunTests(sourceCode, tests)
Tester -> Tester: CompileCode(sourceCode)
Tester -> Sandbox: ExecuteTests(assemblyBytes, tests)
Sandbox -> Sandbox: LoadFromStream()
Sandbox -> Sandbox: ExecuteTestsInIsolation()
Sandbox --> Tester: TestRunResult
Tester --> Controller: TestRunResult
Controller --> Frontend: CodeExecutionResponse
Frontend --> User: Отображает результаты
@enduml
```

## Архитектурные слои

```plantuml
@startuml
package "Presentation Layer" {
  [React Components] as ReactUI
  [ASP.NET Controllers] as Controllers
}

package "Business Logic Layer" {
  [Services] as Services
  [Code Execution] as CodeExec
}

package "Data Access Layer" {
  [Repositories] as Repos
}

package "Domain Model" {
  [Entities] as Entities
  [DTOs] as DTOs
}

ReactUI --> Controllers
Controllers --> Services
Controllers --> CodeExec
Services --> Repos
CodeExec --> Repos
Services --> Entities
CodeExec --> Entities
Repos --> Entities
Controllers --> DTOs
@enduml
```

## Развертывание

```plantuml
@startuml
node "Client Browser" as Browser {
  [React SPA] as ReactApp
}

node "Web Server" as WebServer {
  [ASP.NET Core] as AspNet
  [Static Files] as Static
}

node "Development Environment" as DevEnv {
  [Vite Dev Server] as Vite
}

Browser --> WebServer : HTTP
Browser --> DevEnv : HTTP (Dev Mode)
AspNet --> Static
@enduml
```

## Основные потоки данных

```plantuml
@startuml
actor User

rectangle "Frontend" {
  usecase "Выполнение кода" as UC1
  usecase "Прохождение теста" as UC2
  usecase "Просмотр результатов" as UC3
}

rectangle "Backend" {
  usecase "Компиляция кода" as UC4
  usecase "Выполнение тестов" as UC5
  usecase "Оценка ответов" as UC6
  usecase "Получение заданий" as UC7
}

User --> UC1
User --> UC2
User --> UC3

UC1 --> UC4
UC4 --> UC5
UC2 --> UC6
UC1 --> UC7
UC2 --> UC7
@enduml
```