import styles from './AnswerOption.module.css';
import ReactMarkdown from 'react-markdown';

const AnswerOption = ({
  option,
  questionId,
  selectedAnswer,
  onSelect
}) => {
  return (
    <div 
      className={styles.option}
      onClick={() => onSelect(questionId, option.id)}
    >
      <input
        type="radio"
        id={`q_${questionId}_o_${option.id}`}
        name={`question_${questionId}`}
        checked={Number(selectedAnswer) === Number(option.id)}
        onChange={() => {}}
      />
      <label htmlFor={`q_${questionId}_o_${option.id}`}>
        <span className={styles.text}>
          <ReactMarkdown>{option.text}</ReactMarkdown>
        </span>
      </label>
    </div>
  );
};

export default AnswerOption;