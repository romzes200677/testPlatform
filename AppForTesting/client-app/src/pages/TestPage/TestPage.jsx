import React, { useState, useEffect } from 'react';
import styles from './TestPage.module.css';
import Pagination from '../../components/Pagination/Pagination';
import { fetchQuestions } from '../../services/testService';

const TestPage = () => {
  const [questions, setQuestions] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const questionsPerPage = 10;
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
  const loadQuestions = async () => {
    try {
      const data = await fetchQuestions(currentPage, questionsPerPage);
      setQuestions(data);
    } catch (error) {
      console.error('Error loading questions:', error);
      // Handle error in your component (e.g., show error message)
    } finally {
      setIsLoading(false);
    }
  };

  loadQuestions();
}, [currentPage, questionsPerPage]);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  if (isLoading) {
    return <div className={styles.loading}>Загрузка...</div>;
  }

  if (error) {
    return <div className={styles.error}>Ошибка: {error}</div>;
  }

  return (
    <div className={styles.container}>
      <div className={styles.questionsContainer}>
        {questions.items.map((question) => (
          <div key={question.id} className={styles.question}>
            <h3>{question.text}</h3>
            {/* Отображение вариантов ответов */}
          </div>
        ))}
      </div>
      <Pagination
        currentPage={currentPage}
        totalPages={10} // Это должно прийти с сервера
        onPageChange={handlePageChange}
      />
    </div>
  );
};

export default TestPage;
