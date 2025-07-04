# Описание приложения: Универсальная платформа для тестирования знаний

## Обзор
**Universal Quiz Platform** - это гибкая система для создания и прохождения тестов по различным темам. Изначально разработанная для проверки знаний по C#, платформа эволюционировала в универсальное решение для образовательных учреждений, корпоративного обучения и самостоятельной проверки знаний.

## Ключевые возможности

### Для администраторов/преподавателей
- 🗃️ **Управление тестами через базу данных**
- 📚 **Создание тестов по любым темам**
- 🧩 **Гибкая настройка структуры вопросов**
- 📊 **Анализ результатов тестирования**
- 👥 **Управление пользователями и группами**

### Для пользователей/студентов
- 🚀 **Прохождение тестов в интуитивном интерфейсе**
- ⏱️ **Таймер и контроль времени**
- 📈 **Мгновенные результаты с детализацией**
- 📝 **История пройденных тестов**
- 🏆 **Система достижений и рейтингов**

## Техническая архитектура

```mermaid
graph TD
    A[Frontend - React] --> B[Backend - ASP.NET Core]
    B --> C[Database - PostgreSQL]
    C --> D[Questions]
    C --> E[Tests]
    C --> F[Results]
    C --> G[Users]
    
### Основные компоненты системы

1. **Модуль администрирования**
   - Управление темами и категориями
   - Импорт/экспорт вопросов
   - Настройка параметров тестирования

2. **Движок тестирования**
   - Адаптивная генерация тестов
   - Система подсчета баллов
   - Обработка различных типов вопросов

3. **Аналитическая панель**
   - Визуализация результатов
   - Выявление слабых мест
   - Сравнение с группой

4. **Пользовательские профили**
   - Персональная статистика
   - Достижения и сертификаты
   - Настройка уведомлений

## База данных: Структура хранения вопросов
### Основные таблицы
**Таблица: Topics (Темы)**
| Столбец       | Тип         | Описание                |
|---------------|-------------|-------------------------|
| Id            | INT (PK)    | Уникальный идентификатор|
| Name          | VARCHAR(100)| Название темы          |
| Description   | TEXT        | Описание темы          |
**Таблица: Questions (Вопросы)**
| Столбец       | Тип          | Описание                     |
|---------------|--------------|------------------------------|
| Id            | INT (PK)     | Уникальный идентификатор     |
| TopicId       | INT (FK)     | Ссылка на тему               |
| Text          | TEXT         | Текст вопроса                |
| Type          | VARCHAR(20)  | Тип вопроса (single/multiple)|
| Difficulty    | INT          | Сложность (1-5)              |
| Explanation   | TEXT         | Пояснение к ответу           |
**Таблица: AnswerOptions (Варианты ответов)**
| Столбец       | Тип         | Описание                |
|---------------|-------------|-------------------------|
| Id            | INT (PK)    | Уникальный идентификатор|
| QuestionId    | INT (FK)    | Ссылка на вопрос        |
| Text          | TEXT        | Текст варианта ответа   |
| IsCorrect     | BOOLEAN     | Правильный ответ        |
**Таблица: Tests (Тесты)**
| Столбец       | Тип         | Описание                      |
|---------------|-------------|-------------------------------|
| Id            | INT (PK)    | Уникальный идентификатор      |
| Title         | VARCHAR(100)| Название теста                |
| Description   | TEXT        | Описание теста                |
| TopicId       | INT (FK)    | Основная тема теста           |
| Duration      | INT         | Время на прохождение (минуты) |
_______________________________________________________________

erDiagram
    TOPICS ||--o{ QUESTIONS : contains
    QUESTIONS ||--o{ ANSWER_OPTIONS : has
    TESTS ||--o{ TOPICS : references

    TOPICS {
        int Id PK
        varchar(100) Name
        text Description
    }
    
    QUESTIONS {
        int Id PK
        int TopicId FK
        text Text
        varchar(20) Type
        int Difficulty
        text Explanation
    }
    
    ANSWER_OPTIONS {
        int Id PK
        int QuestionId FK
        text Text
        boolean IsCorrect
    }
    
    TESTS {
        int Id PK
        varchar(100) Title
        text Description
        int TopicId FK
        int Duration
    }

Взаимосвязи таблиц

erDiagram
    TOPICS ||--o{ QUESTIONS : "1 тема → N вопросов"
    QUESTIONS ||--o{ ANSWER_OPTIONS : "1 вопрос → N вариантов"
    TESTS ||--o{ TOPICS : "1 тест → 1 тема"

    TOPICS {
        int Id PK
        varchar Name
        text Description
    }
    QUESTIONS {
        int Id PK
        int TopicId FK
        text Text
        varchar Type
        int Difficulty
        text Explanation
    }
    ANSWER_OPTIONS {
        int Id PK
        int QuestionId FK
        text Text
        boolean IsCorrect
    }
    TESTS {
        int Id PK
        varchar Title
        text Description
        int TopicId FK
        int Duration
        int QuestionCount
    }


