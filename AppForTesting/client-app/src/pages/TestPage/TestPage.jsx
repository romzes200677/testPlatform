import React, { useState, useEffect } from 'react';
import styles from './TestPage.module.css';
import Pagination from '../../components/Pagination/Pagination';
import { fetchQuestions, submitAnswers } from '../../services/testService';
import QuestionCard from '../../components/QuestionCard/QuestionCard';
import { useTestContext } from '../../contexts/TestContext';
import Timer from '../../components/Timer/Timer';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';

const TestPage = () => {
  const [questions, setQuestions] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const questionsPerPage = 10;
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [answers, setAnswers] = useState({});
  const [timeLeft, setTimeLeft] = useState(60 * 60 * 2);
  const { setTestResult } = useTestContext();
  const navigate = useNavigate();

  // Обработчик истечения времени
    const handleTimeUp = () => {
        handleSubmit();
    };
  useEffect(() => {
    const loadQuestions = async () => {
      try {
        const data = await fetchQuestions(currentPage, questionsPerPage);
        setQuestions(data);
      } catch (error) {
        console.error('Error loading questions:', error);
        setError('Failed to load questions');
      } finally {
        setIsLoading(false);
      }
    };

    loadQuestions();
  }, [currentPage, questionsPerPage]);

  const totalPages = Math.ceil(100 / questionsPerPage); // Пример: общее количество вопросов 100
  const handleAnswerSelect = (questionId, answerId) => {
        setAnswers(prev => ({
            ...prev,
            [questionId]: Number(answerId) // Приводим к числу
        }));
    };
  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

   const handleSubmit = async () => {
        try {
            // Преобразуем ответы в нужный формат
            const userAnswers = Object.entries(answers).map(([qId, aId]) => ({
                QuestionId: parseInt(qId),
                SelectedAnswerId: aId
            }));

            // Отправляем на сервер
            const result = await submitAnswers(userAnswers);

            if (!result) {
                throw new Error("Пустой ответ от сервера");
            }

            // Обрабатываем результат
            setTestResult({
                correctAnswers: result.correctAnswers,
                totalQuestions: result.totalQuestions,
                incorrectAnswers: result.incorrectAnswers.map(item => ({
                    Question: {
                        id: item.question.id,
                        text: item.question.text,
                        topic: item.question.topic,
                        options: item.question.options,
                        correctAnswerId: item.question.correctAnswerId,
                        explanation: item.question.explanation
                    },
                    SelectedAnswerId: item.selectedAnswerId,
                }))
            });

            navigate('/results');
        } catch (error) {
            console.error("Ошибка при отправке ответов:", error);
            alert("Произошла ошибка при обработке результатов. Пожалуйста, попробуйте снова.");
        }
    };
  return (
    <div className={styles.testContainer}>
      <div className={styles.testName}>Questions</div>

      {isLoading ? (
        <div>Loading questions...</div>
      ) : error ? (
        <div className={styles.error}>{error}</div>
      ) : (
        <div>
          {questions.items?.map((question) => (
            <QuestionCard
              key={`question_${question.id}`}
              question={question}
              onSelect={handleAnswerSelect}
              selectedAnswer={answers[question.id]}
            />
          ))}
        </div>
      )}

      <Pagination
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={setCurrentPage}
      />
      <Link to="/assignment/math-1">Перейти к решению задач</Link>
       <button
                onClick={handleSubmit}
                disabled={timeLeft === 0}
                className={styles.submitButton}
            >
                 Завершить тест
            </button>
    </div>
  );
};

export default TestPage;
