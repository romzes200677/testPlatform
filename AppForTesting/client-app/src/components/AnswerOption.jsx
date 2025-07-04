import React from 'react';

const AnswerOption = ({
                          option,
                          questionId,
                          selectedAnswer,
                          onSelect
                      }) => {
    return (
        <div className="answer-option">
            <input
                type="radio"
                id={`q_${questionId}_o_${option.id}`}
                name={`question_${questionId}`}
                checked={selectedAnswer === option.id}
                onChange={() => onSelect(questionId, option.id)}
            />
            <label htmlFor={`q_${questionId}_o_${option.id}`}>
                {option.text}
            </label>
        </div>
    );
};

export default AnswerOption;