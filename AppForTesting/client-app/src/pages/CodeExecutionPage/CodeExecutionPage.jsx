import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import CodeEditor from '../../components/CodeEditor/CodeEditor';
import styles from './CodeExecutionPage.module.css';

const CodeExecutionPage = () => {
  const { assignmentId } = useParams();
  const [assignment, setAssignment] = useState(null);
  const [userCode, setUserCode] = useState('');
  const [isExecuting, setIsExecuting] = useState(false);
  const [results, setResults] = useState(null);
  const [error, setError] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

useEffect(() => {
  // Временные данные для тестирования
  const mockAssignment = {
    id: 'math-1',
    title: 'Задание: Калькулятор',
    description: `
      
<!----><span><h2 style="text-align:center;">Артур и поход в магазин</h2>

<p>Сегодня Артур ждёт в гости своего друга Давида. Чтобы подготовиться к встрече, Артур должен посетить два магазина, расположенных рядом с его домом.<br>
От дома к первому магазину ведёт дорожка длиной&nbsp;<code><samp>d<span style="font-size:12px;">1</span></samp></code>&nbsp;метров, ко второму — дорожка длиной &nbsp;<code><samp>d2</samp></code>&nbsp;метров. Также существует дорожка длиной&nbsp;<code><samp>d3</samp></code>&nbsp;метров, напрямую соединяющая два магазина.<br>
Помоги Артуру вычислить минимальное расстояние, которое ему потребуется пройти, чтобы посетить оба магазина и вернуться домой.<br>
Артур всегда начинает путь из дома.&nbsp;Он должен побывать <strong>в обоих магазинах</strong>, перемещаясь <strong>только по имеющимся трём дорожкам</strong>&nbsp;и вернуться домой.<br>
При этом его не смущает, если он пройдёт по одной и той же дорожке несколько раз или окажется в одном и том же магазине дважды — главное, чтобы общий путь был <strong>как можно короче</strong>.</p>

<p>&nbsp;</p>

<p><strong>Формат входных данных:</strong><br>
Три целых числа <code><samp>d<span style="font-size:12px;">1</span></samp></code>, <code><samp>d<span style="font-size:12px;">2</span></samp></code>, <code><samp>d<span style="font-size:12px;">3</span></samp></code>&nbsp;(<em><samp>1 <span class="math-tex"><span><span class="katex"><span class="katex-mathml"><math xmlns="http://www.w3.org/1998/Math/MathML"><semantics><mrow><mo>≤</mo></mrow><annotation encoding="application/x-tex">\le</annotation></semantics></math></span><span class="katex-html" aria-hidden="true"><span class="base"><span class="strut" style="height: 0.77194em; vertical-align: -0.13597em;"></span><span class="mrel">≤</span></span></span></span></span></span> d<sub>1</sub>, d<sub>2</sub>, d<sub>3</sub> <span class="math-tex"><span><span class="katex"><span class="katex-mathml"><math xmlns="http://www.w3.org/1998/Math/MathML"><semantics><mrow><mo>≤</mo></mrow><annotation encoding="application/x-tex">\le</annotation></semantics></math></span><span class="katex-html" aria-hidden="true"><span class="base"><span class="strut" style="height: 0.77194em; vertical-align: -0.13597em;"></span><span class="mrel">≤</span></span></span></span></span></span> 10<sup>8</sup></samp></em>)&nbsp;— длины дорожек:</p>

<ul>
	<li><code><samp>d1</samp></code>&nbsp;— длина дорожки, соединяющей дом Артура и первый магазин;</li>
	<li><code><samp>d2</samp></code>&nbsp;— длина дорожки, соединяющей дом Артура и второй магазин;</li>
	<li><code><samp>d3</samp></code>&nbsp;— длина дорожки, соединяющей два магазина.</li>
</ul>

<p><strong>Формат выходных данных:</strong><br>
Одно число — минимальное количество метров, которое потребуется пройти Артуру,&nbsp;чтобы посетить оба магазина и вернуться домой.</p>

<p>&nbsp;</p>

<p><strong>Рассмотрим первый тест:</strong><br>
На вход получаем&nbsp; строки:</p>

<pre><code class="language-cs hljs language-csharp" data-highlighted="yes"><span class="hljs-number">10</span>
<span class="hljs-number">20</span>
<span class="hljs-number">30</span></code></pre>

<p>Посчитаем длину пути Артура, если он из первого магазина вернется домой, а потом пойдет во второй магазин:</p>

<p><em><samp>(10&nbsp;+ 10)&nbsp;+ (20 + 20) = 60</samp></em></p>

<p>Теперь посчитаем длину пути Артура, если он из первого магазина пойдет во второй магазин, а потом вернется домой:</p>

<p><em><samp>10 + 30 + 20 = 60</samp></em></p>

<p>Теперь посчитаем длину пути Артура, если он из первого магазина пойдет во второй магазин, затем вернется домой через первый магазин:</p>

<p><em><samp>10 + 30 + 30 + 10 = 80</samp></em></p>

<p>Теперь посчитаем длину пути Артура, если из дома он пойдет во второй магазин, затем в первый магазин, затем вернется домой через второй магазин:</p>

<p><em><samp>20 + 30 + 30 + 20 = 100</samp></em></p>

<p><strong>Сравним все четыре результата, чтобы найти наименьший.&nbsp;</strong></p>

<p>Выведем результат в консоль:</p>

<pre><code class="language-cs hljs language-csharp" data-highlighted="yes"><span class="hljs-number">60</span></code></pre>

<pre>&nbsp;</pre>

<p><span style="color:#a03881;"><strong>Примечание: </strong></span>иллюстрация для первого теста:</p>

<p style="text-align:center;"><img alt="" height="180" name="f0b39f9ad01ffba4aefd0c1b9a6fc33bcf679e2e.webp" src="/api/images/house.webp" width="350"></p>

<ol>
	<li>Одним из оптимальных маршрутов является: дом&nbsp;<span class="math-tex"><span><span class="katex"><span class="katex-mathml"><math xmlns="http://www.w3.org/1998/Math/MathML"><semantics><mrow><mo>→</mo></mrow><annotation encoding="application/x-tex">\rightarrow</annotation></semantics></math></span><span class="katex-html" aria-hidden="true"><span class="base"><span class="strut" style="height: 0.36687em; vertical-align: 0em;"></span><span class="mrel">→</span></span></span></span></span></span>&nbsp;первый магазин&nbsp;<span class="math-tex"><span><span class="katex"><span class="katex-mathml"><math xmlns="http://www.w3.org/1998/Math/MathML"><semantics><mrow><mo>→</mo></mrow><annotation encoding="application/x-tex">\rightarrow</annotation></semantics></math></span><span class="katex-html" aria-hidden="true"><span class="base"><span class="strut" style="height: 0.36687em; vertical-align: 0em;"></span><span class="mrel">→</span></span></span></span></span></span>&nbsp;второй магазин&nbsp;<span class="math-tex"><span><span class="katex"><span class="katex-mathml"><math xmlns="http://www.w3.org/1998/Math/MathML"><semantics><mrow><mo>→</mo></mrow><annotation encoding="application/x-tex">\rightarrow</annotation></semantics></math></span><span class="katex-html" aria-hidden="true"><span class="base"><span class="strut" style="height: 0.36687em; vertical-align: 0em;"></span><span class="mrel">→</span></span></span></span></span></span>&nbsp;дом.</li>
	<li>Во втором примере (Тест&nbsp;№2)&nbsp;одним из оптимальных маршрутов является: дом&nbsp;<span class="math-tex"><span><span class="katex"><span class="katex-mathml"><math xmlns="http://www.w3.org/1998/Math/MathML"><semantics><mrow><mo>→</mo></mrow><annotation encoding="application/x-tex">\rightarrow</annotation></semantics></math></span><span class="katex-html" aria-hidden="true"><span class="base"><span class="strut" style="height: 0.36687em; vertical-align: 0em;"></span><span class="mrel">→</span></span></span></span></span></span>&nbsp;первый магазин&nbsp;<span class="math-tex"><span><span class="katex"><span class="katex-mathml"><math xmlns="http://www.w3.org/1998/Math/MathML"><semantics><mrow><mo>→</mo></mrow><annotation encoding="application/x-tex">\rightarrow</annotation></semantics></math></span><span class="katex-html" aria-hidden="true"><span class="base"><span class="strut" style="height: 0.36687em; vertical-align: 0em;"></span><span class="mrel">→</span></span></span></span></span></span>&nbsp;дом&nbsp;<span class="math-tex"><span><span class="katex"><span class="katex-mathml"><math xmlns="http://www.w3.org/1998/Math/MathML"><semantics><mrow><mo>→</mo></mrow><annotation encoding="application/x-tex">\rightarrow</annotation></semantics></math></span><span class="katex-html" aria-hidden="true"><span class="base"><span class="strut" style="height: 0.36687em; vertical-align: 0em;"></span><span class="mrel">→</span></span></span></span></span></span>&nbsp;второй магазин&nbsp;<span class="math-tex"><span><span class="katex"><span class="katex-mathml"><math xmlns="http://www.w3.org/1998/Math/MathML"><semantics><mrow><mo>→</mo></mrow><annotation encoding="application/x-tex">\rightarrow</annotation></semantics></math></span><span class="katex-html" aria-hidden="true"><span class="base"><span class="strut" style="height: 0.36687em; vertical-align: 0em;"></span><span class="mrel">→</span></span></span></span></span></span>&nbsp;дом.</li>
</ol></span></div>
    `,
    mainMethodTemplate: `
using System;

public class Program
{
    static void Main(string[] args)
    {
        ###
    }
}
    `,
    initialCode: `
        var a = long.Parse(Console.ReadLine());
        var b = long.Parse(Console.ReadLine());
        var c = long.Parse(Console.ReadLine());
        var sum = a+b+c;
        sum=100;
        Console.WriteLine(sum);`
  };

  setAssignment(mockAssignment);
  setUserCode(mockAssignment.initialCode);
  setIsLoading(false);
}, [assignmentId]);

 /*  useEffect(() => {
    const fetchAssignment = async () => {
      try {
        setIsLoading(true);
        // Загрузка задания с сервера по ID
        const response = await axios.get(`/api/test/decisions${assignmentId}`);
        setAssignment(response.data);
        setUserCode(response.data.initialCode || '');
      } catch (err) {
        setError('Ошибка загрузки задания');
        console.error(err);
      } finally {
        setIsLoading(false);
      }
    };

    fetchAssignment();
  }, [assignmentId]); */
  const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;
  const handleExecuteCode = async () => {
    if (!assignment) return;
    
    setIsExecuting(true);
    setError(null);
    setResults(null);
    
    try {
      const response =  await axios.post(`${API_BASE_URL}/api/test/execute`, {
        assignmentId: assignment.id,
        mainMethodTemplate: assignment.mainMethodTemplate,
        userCode: userCode,
        assignmentText: assignment.description
      });
      
      setResults(response.data);
    } catch (err) {
      setError(err.response?.data?.message || 'Произошла ошибка при выполнении кода');
    } finally {
      setIsExecuting(false);
    }
  };

  if (isLoading) {
    return <div className={styles.loading}>Загрузка задания...</div>;
  }

  if (error) {
    return <div className={styles.error}>{error}</div>;
  }

  if (!assignment) {
    return <div className={styles.error}>Задание не найдено</div>;
  }

  return (
    <div className={styles.container}>
      <h1 className={styles.title}>{assignment.title}</h1>
      
      <div 
        className={styles.description} 
        dangerouslySetInnerHTML={{ __html: assignment.description }} 
      />
      
      <div className={styles.editorSection}>
        <h2 className={styles.sectionTitle}>Редактор кода</h2>
        <CodeEditor 
          template={assignment.mainMethodTemplate}
          userCode={userCode}
          onCodeChange={setUserCode}
          language="csharp"
        />
      </div>
      
      <button
        onClick={handleExecuteCode}
        disabled={isExecuting}
        className={`${styles.executeButton} ${isExecuting ? styles.disabled : ''}`}
      >
        {isExecuting ? 'Выполнение...' : 'Запустить код'}
      </button>
      
      {results && (
        <div className={styles.resultsContainer}>
          <h2 className={styles.sectionTitle}>Результаты выполнения</h2>
          
          {results.compilationError ? (
            <div className={styles.compilationError}>
              <h3 className={styles.resultTitle}>Ошибка компиляции:</h3>
              <pre className={styles.errorMessage}>{results.compilationError}</pre>
            </div>
          ) : (
            <>
              <div className={`${styles.status} ${results.isSuccess ? styles.success : styles.failure}`}>
                {results.isSuccess ? '✅ Все тесты пройдены успешно' : '❌ Некоторые тесты не пройдены'}
              </div>
              
              {results.output && (
                <div className={styles.outputContainer}>
                  <h3 className={styles.resultTitle}>Вывод программы:</h3>
                  <pre className={styles.output}>{results.output}</pre>
                </div>
              )}
              
              {results.failedTests.length > 0 && (
                <div className={styles.failedTests}>
                  <h3 className={styles.resultTitle}>Неудачные тесты:</h3>
                  <div className={styles.testList}>
                    {results.failedTests.map((test, index) => (
                      <div key={index} className={styles.testItem}>
                        <div className={styles.testName}>{test.testName}</div>
                        {test.errorMessage ? (
                          <div className={styles.testError}>Ошибка: {test.errorMessage}</div>
                        ) : (
                          <>
                            <div className={styles.testRow}>
                              <span>Ожидалось:</span> 
                              <span className={styles.testValue}>{test.expected || 'null'}</span>
                            </div>
                            <div className={styles.testRow}>
                              <span>Получено:</span> 
                              <span className={styles.testValue}>{test.actual || 'null'}</span>
                            </div>
                          </>
                        )}
                      </div>
                    ))}
                  </div>
                </div>
              )}
            </>
          )}
        </div>
      )}
    </div>
  );
};

export default CodeExecutionPage;