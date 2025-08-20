# Диаграммы потоков данных

## Поток данных при выполнении кода

```mermaid
flowchart TD
    A[Пользователь] -->|Отправляет код| B[CodeExecutionPage]
    B -->|HTTP POST /api/test/execute| C[TestController]
    C -->|Передает код и ID задания| D[CodeTester]
    D -->|Получает тесты| E[TestRepository]
    E -->|Возвращает тесты| D
    D -->|Компилирует код| F[Roslyn Compiler]
    F -->|Возвращает результат компиляции| D
    
    subgraph Изолированная среда
    D -->|Выполняет код| G[CodeExecutionSandbox]
    G -->|Создает| H[RemoteExecutor]
    H -->|Выполняет тесты| I[Скомпилированная сборка]
    I -->|Возвращает результаты| H
    H -->|Возвращает результаты| G
    end
    
    G -->|Возвращает результаты| D
    D -->|Возвращает ApiResponse| C
    C -->|HTTP Response| B
    B -->|Отображает результаты| A
```

## Поток данных при прохождении тестов

```mermaid
flowchart TD
    A[Пользователь] -->|Отвечает на вопросы| B[TestPage]
    B -->|HTTP POST /api/test/evaluate| C[TestController]
    C -->|Передает ответы| D[TestService]
    D -->|Получает вопросы| E[TestService.GenerateTest]
    E -->|Возвращает вопросы| D
    D -->|Сравнивает ответы| F[Проверка ответов]
    F -->|Возвращает результаты| D
    D -->|Возвращает TestResult| C
    C -->|HTTP Response| B
    B -->|Отображает результаты| A
```

## Архитектурные слои системы

```mermaid
layeredArchitecture TD
    title Архитектурные слои системы
    
    layer Presentation {
        component SPA["SPA (React)"]
        component API["API Controllers"]
    }
    
    layer BusinessLogic {
        component Services["Services"]
        component CodeExecution["Code Execution"]
    }
    
    layer DataAccess {
        component Repositories["Repositories"]
    }
    
    layer Infrastructure {
        component Sandbox["Sandbox Environment"]
        component Compiler["Roslyn Compiler"]
    }
    
    Presentation --> BusinessLogic
    BusinessLogic --> DataAccess
    BusinessLogic --> Infrastructure
```

## Диаграмма состояний для выполнения кода

```mermaid
stateDiagram-v2
    [*] --> Editing: Пользователь редактирует код
    Editing --> Submitting: Нажатие кнопки "Выполнить"
    Submitting --> Compiling: Отправка на сервер
    Compiling --> Running: Успешная компиляция
    Compiling --> CompilationError: Ошибка компиляции
    Running --> TestsRunning: Запуск тестов
    TestsRunning --> Success: Все тесты пройдены
    TestsRunning --> Failure: Некоторые тесты не пройдены
    TestsRunning --> RuntimeError: Ошибка выполнения
    
    CompilationError --> Editing: Исправление ошибок
    RuntimeError --> Editing: Исправление ошибок
    Failure --> Editing: Исправление логики
    Success --> [*]
```

## Диаграмма развертывания

```mermaid
deploymentDiagram
    title Диаграмма развертывания
    
    Deployment_Node(client, "Клиент", "Браузер") {
        Container(spa, "SPA", "React")
    }
    
    Deployment_Node(server, "Сервер", "Windows/Linux") {
        Deployment_Node(webServer, "Веб-сервер", "ASP.NET Core") {
            Container(api, "API", "ASP.NET Core")
            Container(staticFiles, "Статические файлы", "HTML/JS/CSS")
        }
        
        Deployment_Node(appServer, "Сервер приложений", "ASP.NET Core") {
            Container(services, "Сервисы", "C#")
            Container(sandbox, "Песочница", "AssemblyLoadContext")
        }
        
        Deployment_Node(inMemory, "In-Memory Storage", "") {
            Container(testRepo, "Репозиторий тестов", "Dictionary")
        }
    }
    
    Rel(client, webServer, "HTTPS")
    Rel(webServer, appServer, "Internal")
    Rel(appServer, inMemory, "Internal")
```

## Диаграмма взаимодействия компонентов

```mermaid
sequenceDiagram
    actor User as Пользователь
    participant UI as React UI
    participant API as TestController
    participant Service as TestService
    participant Tester as CodeTester
    participant Repo as TestRepository
    participant Sandbox as CodeExecutionSandbox
    
    User->>UI: Вводит код
    UI->>API: POST /api/test/execute
    API->>Repo: GetTestsForAssignment(id)
    Repo-->>API: Список тестов
    API->>Tester: ExecuteCode(code, tests)
    Tester->>Tester: Компиляция кода
    Tester->>Sandbox: Execute(assembly, tests)
    Sandbox->>Sandbox: Создание изолированной среды
    Sandbox->>Sandbox: Выполнение тестов
    Sandbox-->>Tester: Результаты выполнения
    Tester-->>API: ApiResponse
    API-->>UI: JSON результат
    UI-->>User: Отображение результатов
```