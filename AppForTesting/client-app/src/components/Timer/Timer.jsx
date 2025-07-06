import React, { useEffect, useState } from 'react';
import styles from './Timer.module.css'; // Импорт CSS-модуля

const Timer = ({ timeLeft, onTimeUp }) => {
    const [minutes, setMinutes] = useState(0);
    const [seconds, setSeconds] = useState(0);

    useEffect(() => {
        const mins = Math.floor(timeLeft / 60);
        const secs = timeLeft % 60;

        setMinutes(mins);
        setSeconds(secs);

        if (timeLeft === 0 && onTimeUp) {
            onTimeUp();
        }
    }, [timeLeft, onTimeUp]);

    // Определяем, когда нужно показывать предупреждение и применять стили lowTime
    const isLowTime = timeLeft < 60 && timeLeft > 0;
    const minutesClass = isLowTime ? `${styles.minutes} ${styles.lowTime}` : styles.minutes;
    const secondsClass = isLowTime ? `${styles.seconds} ${styles.lowTime}` : styles.seconds;

    return (
        <div className={styles.container}>
            <div className={styles.display}>
                <span className={minutesClass}>{minutes.toString().padStart(2, '0')}</span>
                <span className={styles.colon}>:</span>
                <span className={secondsClass}>{seconds.toString().padStart(2, '0')}</span>
            </div>

            <div className={styles.timeLabel}>
                <span>мин</span>
                <span>сек</span>
            </div>

            {isLowTime && (
                <div className={styles.warning}>
                    Осталось меньше минуты!
                </div>
            )}
        </div>
    );
};

export default Timer;