import React, { useState, useEffect } from 'react';
import styles from './TestPage.module.css';
import Pagination from '../../components/Pagination/Pagination';
import { fetchQuestions } from '../../services/testService';
import QuestionCard from '../../components/QuestionCard/QuestionCard';

const TestPage = () => {
  const [questions, setQuestions] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const questionsPerPage = 10;
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [answers, setAnswers] = useState({});

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
    </div>
  );
};

export default TestPage;
