import { useTestContext } from '../contexts/TestContext';
import { useNavigate } from 'react-router-dom';

export default function ResultsPage() {
    const { testResult } = useTestContext();
    const navigate = useNavigate();

    // Если результаты еще не загружены
    if (!testResult) {
        return (
            <div className="loading">
                <p>Загрузка результатов...</p>
                <button onClick={() => navigate('/')}>Вернуться к тесту</button>
            </div>
        );
    }

    // Деструктурируем результат для удобства
    const {
        correctAnswers,
        totalQuestions,
        incorrectAnswers = []
    } = testResult;

    // Рассчитываем процент правильных ответов
    const percentage = totalQuestions > 0
        ? Math.round((correctAnswers / totalQuestions) * 100)
        : 0;

    // Группируем ошибки по темам
    const topicsStats = incorrectAnswers.reduce((acc, item) => {
        const topic = item.Question?.Topic || "Без темы";
        acc[topic] = (acc[topic] || 0) + 1;
        return acc;
    }, {});

    return (
        <div className="results">
            <h1>Результаты тестирования</h1>

            <div className="summary-card">
                <h2>Общий результат</h2>
                <p>Правильных ответов: <strong>{correctAnswers}/{totalQuestions}</strong></p>
                <p>Процент правильных: <strong>{percentage}%</strong></p>
            </div>

            {incorrectAnswers.length > 0 && (
                <div className="mistakes">
                    <h2>Ошибки по темам</h2>
                    <div className="topics-stats">
                        {Object.entries(topicsStats).map(([topic, count]) => (
                            <div key={`topic_${topic}`} className="topic-item"> {/* Исправлено: добавлен ключ */}
                                <span className="topic-name">{topic}</span>
                                <span className="topic-errors">{count} ошибок</span>
                            </div>
                        ))}
                    </div>

                    <h3>Детализация ошибок</h3>
                    {incorrectAnswers.map((item, index) => {
                        const question = item.Question || {};
                        const selectedAnswerId = item.SelectedAnswerId;
                        const correctAnswerId = question.CorrectAnswerId;

                        // Находим тексты ответов
                        const selectedAnswerText = question.Options?.find(
                            o => o.Id === selectedAnswerId
                        )?.Text || "Нет ответа";

                        const correctAnswerText = question.Options?.find(
                            o => o.Id === correctAnswerId
                        )?.Text || "Неизвестно";

                        return (
                            <div key={`mistake_${question.Id || index}`} className="mistake-card"> {/* Исправлено: добавлен ключ */}
                                <div className="question-header">
                                    <span className="topic-badge">{question.Topic || "Без темы"}</span>
                                    <h4>{question.Text || "Вопрос не найден"}</h4>
                                </div>

                                <div className="user-answer">
                                    <span className="label">Ваш ответ:</span>
                                    <span className={selectedAnswerId === correctAnswerId ? "correct" : "incorrect"}>
                                        {selectedAnswerText}
                                    </span>
                                </div>

                                {selectedAnswerId !== correctAnswerId && (
                                    <div className="correct-answer">
                                        <span className="label">Правильный ответ:</span>
                                        <span>{correctAnswerText}</span>
                                    </div>
                                )}

                                {question.Explanation && (
                                    <div className="explanation">
                                        <p>{question.Explanation}</p>
                                    </div>
                                )}
                            </div>
                        );
                    })}
                </div>
            )}

            <div className="actions">
                <button onClick={() => navigate('/')}>Пройти снова</button>
            </div>
        </div>
    );
}