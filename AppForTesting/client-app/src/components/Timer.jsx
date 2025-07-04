import React, { useEffect, useState } from 'react';

const Timer = ({ timeLeft, onTimeUp }) => {
    const [minutes, setMinutes] = useState(0);
    const [seconds, setSeconds] = useState(0);

    useEffect(() => {
        const mins = Math.floor(timeLeft / 60);
        const secs = timeLeft % 60;

        setMinutes(mins);
        setSeconds(secs);

        if (timeLeft === 0 && onTimeUp) {
            onTimeUp(); // Вызываем колбэк при истечении времени
        }
    }, [timeLeft, onTimeUp]);

    return (
        <div className="timer-container">
            <div className="timer-display">
                <span className="minutes">{minutes.toString().padStart(2, '0')}</span>
                <span className="colon">:</span>
                <span className="seconds">{seconds.toString().padStart(2, '0')}</span>
            </div>

            <div className="time-label">
                <span>мин</span>
                <span>сек</span>
            </div>

            {timeLeft < 60 && timeLeft > 0 && (
                <div className="time-warning">
                    Осталось меньше минуты!
                </div>
            )}
        </div>
    );
};

export default Timer;