using CSharpTestApp.Models;

namespace CSharpTestApp.Services
{
    public class TestService
    {
        public List<Question> GenerateTest()
        {
            // Реализация генерации вопросов (50 вопросов)
           return new List<Question>
{
    // Раздел 2: Ввод-вывод данных. Переменные (10 вопросов)
    new Question
    {
        Id = 1,
        Topic = "Ввод-вывод данных",
        Text = @"Определи  значение переменной c после выполнения следующего фрагмента программы, не используя среду программирования:


```
        int m = 67; 
        m = m + 13; 
        int n = m / 4 - m / 2; 
        int c = m - n;
```
",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "60" },
            new AnswerOption { Id = 2, Text = "100" },
            new AnswerOption { Id = 3, Text = "-60" },
            new AnswerOption { Id = 4, Text = "32" }
        },
        CorrectAnswerId = 2,
        Explanation = "Необходимо учитывать приоритет выполнения операций и целочисленное деление"
    },
    new Question
    {
        Id = 2,
        Topic = "Переменные",
        Text = "Как объявить переменную целого типа?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "var x = 10;" },
            new AnswerOption { Id = 2, Text = "int x;" },
            new AnswerOption { Id = 3, Text = "string x;" },
            new AnswerOption { Id = 4, Text = "x = int;" }
        },
        CorrectAnswerId = 2,
        Explanation = "Правильный синтаксис: <тип> <имя_переменной>;"
    },
    new Question
    {
        Id = 3,
        Topic = "Ввод данных",
        Text = "Как прочитать целое число из консоли?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Console.GetNumber()" },
            new AnswerOption { Id = 2, Text = "int.Parse(Console.ReadLine())" },
            new AnswerOption { Id = 3, Text = "Console.ReadInteger()" },
            new AnswerOption { Id = 4, Text = "Convert.ToInteger(Console.Read())" },
            new AnswerOption { Id = 5, Text = "Convert.ToInt32(Console.ReadLine())" },
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 4,
        Topic = "Вещественные числа",
        Text = "Какой тип используется для чисел с плавающей точкой двойной точности?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "float" },
            new AnswerOption { Id = 2, Text = "decimal" },
            new AnswerOption { Id = 3, Text = "double" },
            new AnswerOption { Id = 4, Text = "real" }
        },
        CorrectAnswerId = 3,
        Explanation = "double - 64-битное число с плавающей точкой"
    },
    new Question
    {
        Id = 5,
        Topic = "Целые числа",
        Text = "Какой диапазон значений у типа int?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "0 до 4294967295" },
            new AnswerOption { Id = 2, Text = "-32768 до 32767" },
            new AnswerOption { Id = 3, Text = "-2147483648 до 2147483647" },
            new AnswerOption { Id = 4, Text = "0 до 65535" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 6,
        Topic = "Ввод данных",
        Text = "Что вернет Console.ReadLine() при вводе '123'?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Число 123" },
            new AnswerOption { Id = 2, Text = "Символ '1'" },
            new AnswerOption { Id = 3, Text = "Строку \"123\"" },
            new AnswerOption { Id = 4, Text = "Массив char" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 7,
        Topic = "Вещественные числа",
        Text = "Как правильно объявить переменную типа float?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "float x = 5.5;" },
            new AnswerOption { Id = 2, Text = "float x = 5.5f;" },
            new AnswerOption { Id = 3, Text = "float x = 5.5d;" },
            new AnswerOption { Id = 4, Text = "float x = 5;" }
        },
        CorrectAnswerId = 2,
        Explanation = "Суффикс f указывает литерал типа float"
    },
    new Question
    {
        Id = 8,
        Topic = "Переменные",
        Text = "Что делает оператор '%'?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Деление" },
            new AnswerOption { Id = 2, Text = "Умножение" },
            new AnswerOption { Id = 3, Text = "Возведение в степень" },
            new AnswerOption { Id = 4, Text = "Остаток от деления" }
        },
        CorrectAnswerId = 4
    },
    new Question
    {
        Id = 9,
        Topic = "Вывод данных",
        Text = "Как вывести число 5.0 как целое значение, используя оператор явного приведения типов (не вызывая методы)??",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Console.WriteLine((int)5.0);" },
            new AnswerOption { Id = 2, Text = "Console.WriteLine(Convert.ToInt32(5.0));" },
            new AnswerOption { Id = 3, Text = "Console.WriteLine(5.0.ToString(\"0\"));" }
        },
        CorrectAnswerId = 1 // Только вариант 1 использует явное приведение
    },
    new Question
    {
        Id = 10,
        Topic = "Переменные",
        Text = "Какое значение получит переменная: int x = 10 / 4;?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "2" },
            new AnswerOption { Id = 2, Text = "2.5" },
            new AnswerOption { Id = 3, Text = "3" },
            new AnswerOption { Id = 4, Text = "Ошибка компиляции" }
        },
        CorrectAnswerId = 1,
        Explanation = "Целочисленное деление отбрасывает дробную часть"
    },

    // Раздел 3: Условный оператор (8 вопросов)
    new Question
    {
        Id = 11,
        Topic = "Условный оператор",
        Text = "Как записать условие 'a больше 5 И меньше 10'?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "a > 5 && a < 10" },
            new AnswerOption { Id = 2, Text = "a > 5 || a < 10" },
            new AnswerOption { Id = 3, Text = "a > 5 AND a < 10" },
            new AnswerOption { Id = 4, Text = "5 < a < 10" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 12,
        Topic = "Вложенные условия",
        Text = @"Дан код:

if (a > 5) 
    Console.WriteLine(""A"");
else if (a < 3) 
    Console.WriteLine(""B"");
else 
    Console.WriteLine(""C"");

В каком случае выведется ""C""?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Когда a > 5" },
            new AnswerOption { Id = 2, Text = "Когда a < 3" },
            new AnswerOption { Id = 3, Text = "Когда a >= 3 и a <= 5" },
            new AnswerOption { Id = 4, Text = "Никогда" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 13,
        Topic = "Логические операторы",
        Text = "Что вернет выражение: !(true && false)?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "true" },
            new AnswerOption { Id = 2, Text = "false" },
            new AnswerOption { Id = 3, Text = "Ошибку" },
            new AnswerOption { Id = 4, Text = "null" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 14,
        Topic = "Тернарный оператор",
        Text = "Как записать: 'если x > 0, то y=1, иначе y=0'?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "y = if(x>0)1 else 0;" },
            new AnswerOption { Id = 2, Text = "y = x>0 ? 1 : 0;" },
            new AnswerOption { Id = 3, Text = "y = switch(x){case >0:1; default:0};" },
            new AnswerOption { Id = 4, Text = "y = (x>0).ToInt();" }
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 15,
        Topic = "Условный оператор",
        Text = "Сколько условий можно проверить в одном операторе if?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Только одно" },
            new AnswerOption { Id = 2, Text = "Не более двух" },
            new AnswerOption { Id = 3, Text = "Любое количество" },
            new AnswerOption { Id = 4, Text = "Не более трех" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 16,
        Topic = "Логические операторы",
        Text = "Какой оператор означает 'ИЛИ'?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "&&" },
            new AnswerOption { Id = 2, Text = "^^" },
            new AnswerOption { Id = 3, Text = "||" },
            new AnswerOption { Id = 4, Text = "!!" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 17,
        Topic = "Switch-case",
        Text = "Для чего используется оператор switch?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Циклы с счетчиком" },
            new AnswerOption { Id = 2, Text = "Множественный выбор" },
            new AnswerOption { Id = 3, Text = "Обработка исключений" },
            new AnswerOption { Id = 4, Text = "Объявление переменных" }
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 18,
        Topic = "Условный оператор",
        Text = @"Что выведет код указанный ниже.
              
        if (false){
          Console.Write('A')
        else 
          Console.Write('B');
        }
",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "A" },
            new AnswerOption { Id = 2, Text = "B" },
            new AnswerOption { Id = 3, Text = "AB" },
            new AnswerOption { Id = 4, Text = "Ничего" }
        },
        CorrectAnswerId = 2
    },

    // Раздел 4: Операторы цикла (12 вопросов)
    new Question
    {
        Id = 19,
        Topic = "Цикл for",
        Text = "Сколько раз выполнится цикл: for (int i=0; i<5; i++) {...}?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "4" },
            new AnswerOption { Id = 2, Text = "5" },
            new AnswerOption { Id = 3, Text = "6" },
            new AnswerOption { Id = 4, Text = "Бесконечно" }
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 20,
        Topic = "Цикл while",
        Text = "Какой цикл гарантированно выполнится хотя бы один раз?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "for" },
            new AnswerOption { Id = 2, Text = "while" },
            new AnswerOption { Id = 3, Text = "do-while" },
            new AnswerOption { Id = 4, Text = "foreach" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 21,
        Topic = "Операторы break/continue",
        Text = "Что делает оператор break в цикле?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Переходит к следующей итерации" },
            new AnswerOption { Id = 2, Text = "Завершает текущую итерацию" },
            new AnswerOption { Id = 3, Text = "Завершает цикл" },
            new AnswerOption { Id = 4, Text = "Перезапускает цикл" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 22,
        Topic = "Вложенные циклы",
        Text = @"Сколько всего итераций выполнит код: 
        
        for (int i=0; i<3; i++) 
            for (int j=0; j<2; j++) 
                {...}?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "5" },
            new AnswerOption { Id = 2, Text = "6" },
            new AnswerOption { Id = 3, Text = "3" },
            new AnswerOption { Id = 4, Text = "2" }
        },
        CorrectAnswerId = 2,
        Explanation = "3 * 2 = 6 итераций"
    },
    new Question
    {
        Id = 23,
        Topic = "Цикл while",
        Text = "Какой код выведет числа от 1 до 3?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "int i=0; while(i<3) Console.WriteLine(i++);" },
            new AnswerOption { Id = 2, Text = "int i=1; while(i<=3) Console.WriteLine(i++);" },
            new AnswerOption { Id = 3, Text = "int i=1; while(i<3) Console.WriteLine(i);" },
            new AnswerOption { Id = 4, Text = "int i=0; do Console.WriteLine(++i); while(i<3);" }
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 24,
        Topic = "Операторы break/continue",
        Text = "Что делает оператор continue?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Завершает программу" },
            new AnswerOption { Id = 2, Text = "Выходит из цикла" },
            new AnswerOption { Id = 3, Text = "Пропускает текущую итерацию" },
            new AnswerOption { Id = 4, Text = "Возобновляет работу цикла" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 25,
        Topic = "Цикл for",
        Text = "Какой элемент заголовка for обязателен?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Инициализация" },
            new AnswerOption { Id = 2, Text = "Условие" },
            new AnswerOption { Id = 3, Text = "Инкремент" },
            new AnswerOption { Id = 4, Text = "Все элементы опциональны" }
        },
        CorrectAnswerId = 4,
        Explanation = "Можно написать: for(;;) - бесконечный цикл"
    },
    new Question
    {
        Id = 26,
        Topic = "Минимаксные задачи",
        Text = "Как найти минимальное значение в массиве?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Использовать Math.Min()" },
            new AnswerOption { Id = 2, Text = "Отсортировать массив по возрастанию" },
            new AnswerOption { Id = 3, Text = "Перебрать элементы циклом" },
            new AnswerOption { Id = 4, Text = "Варианты 2 и 3" }
        },
        CorrectAnswerId = 4
    },
    new Question
    {
        Id = 27,
        Topic = "Цикл while",
        Text = "Как обеспечить выход из бесконечного цикла?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Использовать break" },
            new AnswerOption { Id = 2, Text = "Изменить условие цикла" },
            new AnswerOption { Id = 3, Text = "Добавить sleep между итерациями" },
            new AnswerOption { Id = 4, Text = "Варианты 1 и 2" } // Правильный ответ
        },
        CorrectAnswerId = 4,
        Explanation = "Sleep только приостанавливает цикл, но не обеспечивает выход. Для выхода нужно break или изменение условия."
    },
    new Question
    {
        Id = 28,
        Topic = "Цикл for",
        Text = "Как записать цикл от 10 до 1 включительно?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "for (int i=10; i>1; i--)" },
            new AnswerOption { Id = 2, Text = "for (int i=10; i>=1; i--)" },
            new AnswerOption { Id = 3, Text = "for (int i=10; i==1; i-)" },
            new AnswerOption { Id = 4, Text = "for (int i=10; i<=1; i++)" }
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 29,
        Topic = "Вложенные циклы",
        Text = @"Что выведет код: 
        
          for (int i=1; i<=2; i++) 
            for (int j=1; j<=i; j++) 
                Console.Write(j);?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "11" },
            new AnswerOption { Id = 2, Text = "112" },
            new AnswerOption { Id = 3, Text = "121" },
            new AnswerOption { Id = 4, Text = "11212" }
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 30,
        Topic = "Цикл for",
        Text = "Какие переменные сохранят значение после цикла: for (int i=0; i<5; i++){...}?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Переменная i" },
            new AnswerOption { Id = 2, Text = "Переменные, объявленные внутри цикла" },
            new AnswerOption { Id = 3, Text = "Все созданные в цикле переменные" },
            new AnswerOption { Id = 4, Text = "Только переменные, объявленные до цикла" },
            new AnswerOption { Id = 5, Text = "Варианты 1 и 2" }
        },
        CorrectAnswerId = 4, // Только переменные ДО цикла
        Explanation = "Переменные в заголовке цикла (i) и внутри его тела уничтожаются после выполнения. Сохраняются только переменные, объявленные до цикла."
    },

    // Раздел 5: Строки и символы (5 вопросов)
    new Question
    {
        Id = 31,
        Topic = "Символы",
        Text = "Какой тип используется для одиночного символа?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "string" },
            new AnswerOption { Id = 2, Text = "char" },
            new AnswerOption { Id = 3, Text = "symbol" },
            new AnswerOption { Id = 4, Text = "letter" }
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 32,
        Topic = "Строки",
        Text = "Как получить длину строки?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "str.Length" },
            new AnswerOption { Id = 2, Text = "str.Count" },
            new AnswerOption { Id = 3, Text = "str.Size" },
            new AnswerOption { Id = 4, Text = "len(str)" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 33,
        Topic = "Строки",
        Text = "Как объединить две строки?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "str1 + str2" },
            new AnswerOption { Id = 2, Text = "str1.Concat(str2)" },
            new AnswerOption { Id = 3, Text = "String.Join(\"\", str1, str2)" },
            new AnswerOption { Id = 4, Text = "Любой вариант" }
        },
        CorrectAnswerId = 4
    },
    new Question
    {
        Id = 34,
        Topic = "Символы",
        Text = "Как преобразовать символ в верхний регистр?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "char.ToUpper(ch)" },
            new AnswerOption { Id = 2, Text = "ch.ToUpper()" },
            new AnswerOption { Id = 3, Text = "Char.UpperCase(ch)" },
            new AnswerOption { Id = 4, Text = "ch.Upper()" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 35,
        Topic = "Строки",
        Text = "Что вернет Hello.Substring(1, 3)?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Hel" },
            new AnswerOption { Id = 2, Text = "ell" },
            new AnswerOption { Id = 3, Text = "llo" },
            new AnswerOption { Id = 4, Text = "lo" }
        },
        CorrectAnswerId = 2,
        Explanation = "Начиная с индекса 1, взять 3 символа: e,l,l"
    },

    // Раздел 6: Функции (8 вопросов)
    new Question
    {
        Id = 36,
        Topic = "Функции",
        Text = "Как объявить функцию без возвращаемого значения?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "void Func()" },
            new AnswerOption { Id = 2, Text = "function Func()" },
            new AnswerOption { Id = 3, Text = "def Func()" },
            new AnswerOption { Id = 4, Text = "null Func()" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 37,
        Topic = "Параметры функций",
        Text = "Как передать параметр по ссылке?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "void Func(int param)" },
            new AnswerOption { Id = 2, Text = "void Func(ref int param)" },
            new AnswerOption { Id = 3, Text = "void Func(out int param)" },
            new AnswerOption { Id = 4, Text = "Варианты 2 и 3" }
        },
        CorrectAnswerId = 4
    },
    new Question
    {
        Id = 38,
        Topic = "Возврат значений",
        Text = "Как вернуть значение из функции?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "return value;" },
            new AnswerOption { Id = 2, Text = "break value;" },
            new AnswerOption { Id = 3, Text = "exit(value);" },
            new AnswerOption { Id = 4, Text = "result = value;" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 39,
        Topic = "Виды функций",
        Text = "Можно ли вызвать функцию до её объявления?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Да, всегда" },
            new AnswerOption { Id = 2, Text = "Только в том же классе" },
            new AnswerOption { Id = 3, Text = "Нет" },
            new AnswerOption { Id = 4, Text = "Только если функция статическая" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 40,
        Topic = "Оператор return",
        Text = "Что происходит после выполнения return?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Функция продолжает выполнение" },
            new AnswerOption { Id = 2, Text = "Управление возвращается в вызывающий код" },
            new AnswerOption { Id = 3, Text = "Программа завершается" },
            new AnswerOption { Id = 4, Text = "Выполняется следующий оператор" }
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 41,
        Topic = "Параметры функций",
        Text = "Сколько параметров может принимать функция?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Не более 5" },
            new AnswerOption { Id = 2, Text = "Не более 10" },
            new AnswerOption { Id = 3, Text = "Любое количество" },
            new AnswerOption { Id = 4, Text = "Зависит от типа" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 42,
        Topic = "Виды функций",
        Text = "Какой тип функции не возвращает значение?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "int" },
            new AnswerOption { Id = 2, Text = "string" },
            new AnswerOption { Id = 3, Text = "void" },
            new AnswerOption { Id = 4, Text = "bool" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 43,
        Topic = "Функции",
        Text = "Что такое параметр функции?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Локальная переменная" },
            new AnswerOption { Id = 2, Text = "Глобальная переменная" },
            new AnswerOption { Id = 3, Text = "Значение, передаваемое в функцию" },
            new AnswerOption { Id = 4, Text = "Результат работы функции" }
        },
        CorrectAnswerId = 3
    },

    // Раздел 7: Массивы (7 вопросов)
    new Question
    {
        Id = 44,
        Topic = "Массивы",
        Text = "Как создать и сразу инициализировать массив из 3 целых чисел?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "int[] arr = {1,2,3};" },
            new AnswerOption { Id = 2, Text = "int arr = [1,2,3];" },
            new AnswerOption { Id = 3, Text = "array arr = new array(3);" },
            new AnswerOption { Id = 4, Text = "int[] arr = new int[3];" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 45,
        Topic = "Индексация",
        Text = "Как получить первый элемент массива?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "arr[0]" },
            new AnswerOption { Id = 2, Text = "arr[1]" },
            new AnswerOption { Id = 3, Text = "arr.First()" },
            new AnswerOption { Id = 4, Text = "arr(0)" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 46,
        Topic = "Многомерные массивы",
        Text = "Как объявить двумерный массив 2x3?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "int[,] matrix = new int[2,3];" },
            new AnswerOption { Id = 3, Text = "int[2] matrix = new int[3];" },
            new AnswerOption { Id = 4, Text = "int matrix = [2,3];" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 47,
        Topic = "Массивы",
        Text = "Как получить количество элементов массива?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "arr.Count()" },
            new AnswerOption { Id = 2, Text = "arr.Length" },
            new AnswerOption { Id = 3, Text = "arr.Size" },
            new AnswerOption { Id = 4, Text = "arr.Count" }
        },
        CorrectAnswerId = 2
    },
    new Question
    {
        Id = 48,
        Topic = "Индексация",
        Text = "Что вернет arr[arr.Length]?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Последний элемент" },
            new AnswerOption { Id = 2, Text = "Первый элемент" },
            new AnswerOption { Id = 3, Text = "Ошибку IndexOutOfRange" },
            new AnswerOption { Id = 4, Text = "0" }
        },
        CorrectAnswerId = 3
    },
    new Question
    {
        Id = 49,
        Topic = "Многомерные массивы",
        Text = "Как обратиться к элементу в строке 1, столбце 2 двумерного массива?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "matrix[0][1]" },
            new AnswerOption { Id = 2, Text = "matrix[1,2]" },
            new AnswerOption { Id = 3, Text = "matrix[1][2]" },
            new AnswerOption { Id = 4, Text = "matrix[0,1]" }
        },
        CorrectAnswerId = 4,
        Explanation = "Индексация с 0: строка 0, столбец 1"
    },
    new Question
    {
        Id = 50,
        Topic = "Массивы",
        Text = "Что такое массив?",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = "Набор однотипных данных" },
            new AnswerOption { Id = 2, Text = "Таблица значений" },
            new AnswerOption { Id = 3, Text = "Коллекция разных типов" },
            new AnswerOption { Id = 4, Text = "Специальный класс" }
        },
        CorrectAnswerId = 1
    },
    new Question
    {
        Id = 51,
        Topic = "Циклы",
        Text = @"

Сумма последних трех цифр

```
        Дано натуральное число. Найди сумму последних трех цифр.
        Формат входных данных:
            Одно натуральное число, не превосходящее 10 в 9 степени (1000000000).

        Формат выходных данных:
            Одно число - сумма последних трех цифр исходного числа.

        Рассмотрим первый тест:
        На вход получаем  число:

        65421
        Последние три цифры числа 4 2 1. Найдем их сумму: 4 + 2 + 1 = 7.

        Выведем результат в консоль:
            7

        Примеры:

        Sample Input:
        65421
        Sample Output 1:
            7

        Sample Input 2:
        25
        Sample Output 2:
            7

        Sample Input 3:
        951
        Sample Output 3:
            15
```
",
        Options = new List<AnswerOption>
        {
            new AnswerOption { Id = 1, Text = @"Решение с автором" }
        },
        CorrectAnswerId = 1
    }
};
        }

        public TestResult EvaluateTest(List<UserAnswer> userAnswers, List<Question> questions)
        {
            int correctCount = 0;
            var incorrectAnswers = new List<QuestionResult>();
            
            // Создаем словарь для быстрого поиска вопросов по ID
            var questionDict = questions.ToDictionary(q => q.Id);
            
            foreach (var userAnswer in userAnswers)
            {
                if (questionDict.TryGetValue(userAnswer.QuestionId, out var question))
                {
                    if (userAnswer.SelectedAnswerId == question.CorrectAnswerId)
                    {
                        correctCount++;
                    }
                    else
                    {
                        incorrectAnswers.Add(new QuestionResult
                        {
                            Question = question,
                            SelectedAnswerId = userAnswer.SelectedAnswerId
                        });
                    }
                }
            }
            
            return new TestResult
            {
                TotalQuestions = questions.Count, // Общее количество вопросов
                CorrectAnswers = correctCount,    // Количество правильных ответов
                IncorrectAnswers = incorrectAnswers
            };
        }

        public async Task<List<UnitTest>> GetTests(string userId)
        {
            return await Task.FromResult(new List<UnitTest>
            {
                // Тесты для калькулятора
                new UnitTest
                {
                    Name = "Addition of positive numbers",
                    MethodName = "Add",
                    Parameters = new object[] { 5, 3 },
                    ExpectedResult = 8
                },
                new UnitTest
                {
                    Name = "Addition with zero",
                    MethodName = "Add",
                    Parameters = new object[] { 5, 0 },
                    ExpectedResult = 5
                },
                new UnitTest
                {
                    Name = "Negative numbers addition",
                    MethodName = "Add",
                    Parameters = new object[] { -5, -3 },
                    ExpectedResult = -8
                },
                
                // Тесты для строковых операций
                new UnitTest
                {
                    Name = "String concatenation",
                    MethodName = "Concat",
                    Parameters = new object[] { "Hello", "World" },
                    ExpectedResult = "HelloWorld"
                },
                new UnitTest
                {
                    Name = "Empty string concatenation",
                    MethodName = "Concat",
                    Parameters = new object[] { "Test", "" },
                    ExpectedResult = "Test"
                },
                
                // Тест для проверки null
                new UnitTest
                {
                    Name = "Null handling test",
                    MethodName = "SafeConcat",
                    Parameters = new object[] { null, "value" },
                    ExpectedResult = "nullvalue"
                },
                new UnitTest
                {
                    Name = "Check Tst",
                    MethodName = "Tst",
                    Parameters = new object[] { "ddd", "value" },
                    ExpectedResult = "cde"
                }
            });
        }
    }
}