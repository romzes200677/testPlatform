import AnswerOption from '../AnswerOption/AnswerOption';
import styles from './QuestionCard.module.css';
import ReactMarkdown from 'react-markdown';

const QuestionCard = ({ question, onSelect, selectedAnswer }) => {
  if (!question) {
    return <div>Вопрос не загружен</div>;
  }

  const options = question.options || [];

  return (
    <div className={styles.card}>
      <div className={styles.header}>
        <span className={styles.id}>Вопрос {question.id || '?'}</span>
        <h3 className={styles.title}>{question.topic || 'Без темы'}</h3>
        
        <div className={styles.text}>
          <ReactMarkdown>
            {question.text || 'Текст вопроса отсутствует'}
          </ReactMarkdown>
        </div>
      </div>

      <div className={styles.optionsContainer}>
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
          <div className={styles.noOptions}>
            Варианты ответа не загружены
          </div>
        )}
      </div>
    </div>
  );
};

export default QuestionCard;