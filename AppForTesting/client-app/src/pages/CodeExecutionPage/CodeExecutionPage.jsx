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
      <h2>Создайте простой калькулятор</h2>
      <p>Реализуйте класс Calculator со следующими методами:</p>
      <ul>
        <li><code>Add(int a, int b)</code> - возвращает сумму чисел</li>
        <li><code>Subtract(int a, int b)</code> - возвращает разность чисел</li>
        <li><code>Multiply(int a, int b)</code> - возвращает произведение чисел</li>
        <li><code>Divide(int a, int b)</code> - возвращает результат деления</li>
      </ul>
      <p>В методе Main создайте экземпляр калькулятора и продемонстрируйте его работу.</p>
    `,
    mainMethodTemplate: `
using System;

public class MainClass
{
    public static void Main()
    {
        Calculator calc = new Calculator();
        // Демонстрация работы калькулятора
        ###
    }
}

public class Calculator
{
    // Реализуйте методы калькулятора здесь
}
    `,
    initialCode: `Console.WriteLine("Сумма: " + calc.Add(5, 3));
Console.WriteLine("Разность: " + calc.Subtract(5, 3));
Console.WriteLine("Произведение: " + calc.Multiply(5, 3));
Console.WriteLine("Деление: " + calc.Divide(5, 3));`
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