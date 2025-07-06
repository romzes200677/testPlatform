import { useTestContext } from '../../contexts/TestContext';
import { useNavigate } from 'react-router-dom';
import styles from './ResultsPage.module.css'; // Импорт CSS-модуля

export default function ResultsPage() {
    const { testResult } = useTestContext();
    const navigate = useNavigate();

    if (!testResult) {
        return (
            <div className={styles.loading}>
                <p>Загрузка результатов...</p>
                <button onClick={() => navigate('/')}>Вернуться к тесту</button>
            </div>
        );
    }

    const {
        correctAnswers,
        totalQuestions,
        incorrectAnswers = []
    } = testResult;

    const percentage = totalQuestions > 0
        ? Math.round((correctAnswers / totalQuestions) * 100)
        : 0;

    const topicsStats = incorrectAnswers.reduce((acc, item) => {
        const topic = item.Question?.topic || "Без темы";
        acc[topic] = (acc[topic] || 0) + 1;
        return acc;
    }, {});

    return (
        <div className={styles.container}>
            <h1>Результаты тестирования</h1>

            <div className={styles.summary}>
                <h2>Общий результат</h2>
                <p>Правильных ответов: <strong>{correctAnswers}/{totalQuestions}</strong></p>
                <p>Процент правильных: <strong>{percentage}%</strong></p>
            </div>

            {incorrectAnswers.length > 0 && (
                <div className={styles.mistakes}>
                    <h2>Ошибки по темам</h2>
                    <div className={styles.stats}>
                        {Object.entries(topicsStats).map(([topic, count]) => (
                            <div key={`topic_${topic}`} className={styles.topic}>
                                <span className={styles.topicName}>{topic}</span>
                                <span className={styles.topicErrors}>{count} ошибок</span>
                            </div>
                        ))}
                    </div>

                    <h3>Детализация ошибок</h3>
                    {incorrectAnswers.map((item, index) => {
                        const question = item.Question || {};
                        const selectedAnswerId = item.SelectedAnswerId;
                        const correctAnswerId = question.correctAnswerId;
                        
                        const selectedAnswerText = question.options?.find(
                            o => o.id === selectedAnswerId
                        )?.text || "Нет ответа";
                        
                        const correctAnswerText = question.options?.find(
                            o => o.id === correctAnswerId
                        )?.text || "Неизвестно";

                        return (
                            <div key={`mistake_${question.id || index}`} className={styles.mistakeCard}>
                                <div className={styles.mistakeHeader}>
                                    <span className={styles.badge}>{question.topic || "Без темы"}</span>
                                    <h4>{question.text || "Вопрос не найден"}</h4>
                                </div>

                                <div className={styles.userAnswer}>
                                    <span className={styles.label}>Ваш ответ:</span>
                                    <span className={`${styles.answerText} ${selectedAnswerId === correctAnswerId ? styles.correctAnswerText : styles.userAnswerText}`}>
                                        {selectedAnswerText}
                                    </span>
                                </div>

                                {selectedAnswerId !== correctAnswerId && (
                                    <div className={styles.correctAnswer}>
                                        <span className={styles.label}>Правильный ответ:</span>
                                        <span className={styles.correctAnswerText}>{correctAnswerText}</span>
                                    </div>
                                )}

                                {question.explanation && (
                                    <div className={styles.explanation}>
                                        <p>{question.explanation}</p>
                                    </div>
                                )}
                            </div>
                        );
                    })}
                </div>
            )}

            <div className={styles.actions}>
                <button onClick={() => navigate('/')}>Пройти снова</button>
            </div>
        </div>
    );
}