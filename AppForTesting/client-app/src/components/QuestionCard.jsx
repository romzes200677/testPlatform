import React from 'react';
import AnswerOption from './AnswerOption';

const QuestionCard = ({ question, onSelect, selectedAnswer }) => {
    // Проверяем наличие вопроса и его опций
    if (!question) {
        return <div className="question-error">Вопрос не загружен</div>;
    }

    // Получаем опции или используем пустой массив по умолчанию
    const options = question.options || [];

    return (
        <div className="question-card">
            <div className="question-header">
                <span className="question-id">Вопрос {question.Id || '?'}</span>
                <h3>{question.text || 'Текст вопроса отсутствует'}</h3>
            </div>

            <div className="options-container">
                {options.length > 0 ? (
                    options.map(option => (
                        <AnswerOption
                            key={`option_${question.id}_${option.id}`}
                            option={option}
                            questionId={question.id}
                            selectedAnswer={selectedAnswer}
                            onSelect={onSelect}
                        />
                    ))
                ) : (
                    <div className="no-options">
                        Варианты ответа не загружены
                    </div>
                )}
            </div>
        </div>
    );
};

export default QuestionCard;