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
                id={`q_${questionId}_o_${option.Id}`}
                name={`question_${questionId}`}
                checked={selectedAnswer === option.Id}
                onChange={() => onSelect(questionId, option.Id)}
            />
            <label htmlFor={`q_${questionId}_o_${option.Id}`}>
                {option.Text}
            </label>
        </div>
    );
};

export default AnswerOption;