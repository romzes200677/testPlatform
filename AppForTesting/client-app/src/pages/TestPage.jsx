import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useTestContext } from '../contexts/TestContext';
import { fetchQuestions, submitAnswers } from '../services/testService';
import Timer from '../components/Timer';
import QuestionCard from '../components/QuestionCard';

export default function TestPage() {
    const [questions, setQuestions] = useState([]);
    const [answers, setAnswers] = useState({});
    const [timeLeft, setTimeLeft] = useState(60 * 60);
    const { setTestResult } = useTestContext();
    const navigate = useNavigate();

    // Обработчик истечения времени
    const handleTimeUp = () => {
        handleSubmit();
    };

    useEffect(() => {
        const loadQuestions = async () => {
            try {
                const data = await fetchQuestions();

                // Добавляем временные ID для вопросов без ID
                const processedQuestions = data.map((q, index) => ({
                    ...q,
                    // Если у вопроса нет ID, используем индекс + 1000
                    Id: q.id || index + 1000
                }));

                setQuestions(processedQuestions);
            } catch (error) {
                console.error("Ошибка загрузки вопросов:", error);
                alert(`${error.message}\n\nПопробуйте перезагрузить страницу.`);
                setQuestions([]);
            }
        };

        loadQuestions();

        // Таймер обратного отсчета
        const timerId = setInterval(() => {
            setTimeLeft(prev => {
                if (prev <= 1) {
                    clearInterval(timerId);
                    handleTimeUp();
                    return 0;
                }
                return prev - 1;
            });
        }, 1000);

        return () => clearInterval(timerId);
    }, []);

   const handleAnswerSelect = (questionId, answerId) => {
  setAnswers(prev => ({
    ...prev,
    [questionId]: Number(answerId) // Приводим к числу
  }));
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
                    Question: item.question,
                    SelectedAnswerId: item.selectedAnswerId,
                    SelectedAnswerText: item.question.options.find(
                        o => o.id === item.selectedAnswerId
                    )?.text || "Нет ответа",
                    CorrectAnswerText: item.question.options.find(
                        o => o.Id === item.question.correctAnswerId
                    )?.text || "Неизвестно",
                    Explanation: item.question.explanation
                }))
            });

            navigate('/results');
        } catch (error) {
            console.error("Ошибка при отправке ответов:", error);
            alert("Произошла ошибка при обработке результатов. Пожалуйста, попробуйте снова.");
        }
    };

    return (
        <div className="test-container">
            <Timer
                timeLeft={timeLeft}
                onTimeUp={handleTimeUp}
            />

            {questions.length > 0 ? (
                questions.map(question => (
                    <QuestionCard
                        key={`question_${question.id}`}
                        question={question}
                        onSelect={handleAnswerSelect}
                        selectedAnswer={answers[question.id]}
                    />
                ))
            ) : (
                <div className="loading-questions">
                    <div className="spinner"></div>
                    <p>Загрузка вопросов...</p>
                </div>
            )}

            <button
                onClick={handleSubmit}
                disabled={timeLeft === 0}
                className="submit-btn"
            >
                {timeLeft === 0 ? 'Время вышло!' : 'Завершить тест'}
            </button>
        </div>
    );
}