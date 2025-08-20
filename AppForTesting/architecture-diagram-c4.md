# Архитектура проекта CSharpTestApp (C4 модель)

## Контекстная диаграмма (C1)

```mermaid
C4Context
    title Контекстная диаграмма системы CSharpTestApp

    Person(student, "Студент", "Пользователь, который проходит тесты и выполняет задания по программированию")
    
    System(testPlatform, "Платформа тестирования", "Позволяет проходить тесты и выполнять задания по программированию с автоматической проверкой")
    
    System_Ext(compiler, "Компилятор C#", "Компилирует и выполняет код на C#")

    Rel(student, testPlatform, "Проходит тесты и выполняет задания")
    Rel(testPlatform, compiler, "Использует для компиляции и выполнения кода")
```

## Диаграмма контейнеров (C2)

```mermaid
C4Container
    title Диаграмма контейнеров системы CSharpTestApp

    Person(student, "Студент", "Пользователь, который проходит тесты и выполняет задания по программированию")
    
    System_Boundary(testPlatform, "Платформа тестирования") {
        Container(webApp, "Веб-приложение", "ASP.NET Core", "Обрабатывает HTTP-запросы, предоставляет API и статические файлы")
        Container(spaClient, "SPA-клиент", "React", "Предоставляет пользовательский интерфейс для прохождения тестов и выполнения заданий")
        Container(codeExecutor, "Исполнитель кода", "C#", "Компилирует и выполняет код в изолированной среде")
        Container(testRepository, "Репозиторий тестов", "C#", "Хранит и предоставляет доступ к тестам и заданиям")
    }
    
    System_Ext(compiler, "Компилятор C#", "Компилирует и выполняет код на C#")

    Rel(student, spaClient, "Взаимодействует через браузер", "HTTPS")
    Rel(spaClient, webApp, "Отправляет запросы", "JSON/HTTPS")
    Rel(webApp, codeExecutor, "Передает код для выполнения")
    Rel(webApp, testRepository, "Получает тесты и задания")
    Rel(codeExecutor, compiler, "Использует для компиляции кода")
```

## Диаграмма компонентов (C3) - Веб-приложение

```mermaid
C4Component
    title Диаграмма компонентов веб-приложения

    Container_Boundary(webApp, "Веб-приложение") {
        Component(testController, "TestController", "ASP.NET Core Controller", "Обрабатывает HTTP-запросы, связанные с тестами и выполнением кода")
        Component(testService, "TestService", "C# Service", "Предоставляет бизнес-логику для работы с тестами")
        Component(codeTester, "CodeTester", "C# Service", "Компилирует и выполняет код")
        Component(sandboxExecutor, "CodeExecutionSandbox", "C# Service", "Выполняет код в изолированной среде")
        Component(testRepo, "TestRepository", "C# Repository", "Предоставляет доступ к тестам и заданиям")
    }

    Container(spaClient, "SPA-клиент", "React", "Предоставляет пользовательский интерфейс")
    System_Ext(compiler, "Компилятор C#", "Компилирует код на C#")

    Rel(spaClient, testController, "Отправляет запросы", "JSON/HTTPS")
    Rel(testController, testService, "Использует")
    Rel(testController, codeTester, "Использует")
    Rel(testController, testRepo, "Использует")
    Rel(codeTester, sandboxExecutor, "Использует")
    Rel(sandboxExecutor, compiler, "Использует")
```

## Диаграмма компонентов (C3) - SPA-клиент

```mermaid
C4Component
    title Диаграмма компонентов SPA-клиента

    Container_Boundary(spaClient, "SPA-клиент") {
        Component(app, "App", "React Component", "Корневой компонент приложения")
        Component(testPage, "TestPage", "React Component", "Страница для прохождения тестов")
        Component(codeExecPage, "CodeExecutionPage", "React Component", "Страница для выполнения заданий по программированию")
        Component(resultsPage, "ResultsPage", "React Component", "Страница с результатами тестов")
        Component(codeEditor, "CodeEditor", "React Component", "Редактор кода")
        Component(testService, "testService", "JavaScript Module", "Модуль для взаимодействия с API")
    }

    Container(webApp, "Веб-приложение", "ASP.NET Core", "Обрабатывает HTTP-запросы")

    Rel(app, testPage, "Включает")
    Rel(app, codeExecPage, "Включает")
    Rel(app, resultsPage, "Включает")
    Rel(codeExecPage, codeEditor, "Использует")
    Rel(testPage, testService, "Использует")
    Rel(codeExecPage, testService, "Использует")
    Rel(resultsPage, testService, "Использует")
    Rel(testService, webApp, "Отправляет запросы", "JSON/HTTPS")
```

## Диаграмма кода (C4) - CodeTester и CodeExecutionSandbox

```mermaid
C4Code
    title Диаграмма кода компонентов CodeTester и CodeExecutionSandbox

    Component_Boundary(codeTester, "CodeTester") {
        Class(iCodeTester, "ICodeTester", "Interface", "Интерфейс для компиляции и выполнения кода")
        Class(codeTesterImpl, "CodeTester", "Class", "Реализация интерфейса ICodeTester")
        Class(compilationResult, "CompilationResult", "Class", "Результат компиляции кода")
        Class(testRunResult, "TestRunResult", "Class", "Результат выполнения тестов")
    }

    Component_Boundary(sandbox, "CodeExecutionSandbox") {
        Class(sandboxClass, "CodeExecutionSandbox", "Class", "Изолированная среда для выполнения кода")
        Class(remoteExecutor, "RemoteExecutor", "Class", "Выполняет тесты в изолированной среде")
    }

    Component_Boundary(models, "Models") {
        Class(unitTest, "UnitTest", "Class", "Модель юнит-теста")
    }

    Rel(iCodeTester, codeTesterImpl, "Реализуется")
    Rel(codeTesterImpl, compilationResult, "Создает")
    Rel(codeTesterImpl, testRunResult, "Возвращает")
    Rel(codeTesterImpl, sandboxClass, "Использует")
    Rel(sandboxClass, remoteExecutor, "Создает")
    Rel(remoteExecutor, unitTest, "Использует")
    Rel(remoteExecutor, testRunResult, "Возвращает")
```