const API_BASE_URL = 'http://localhost:5231/api';

export const fetchQuestions = async () => {
    try {
        const response = await fetch(`${API_BASE_URL}/test/questions`);

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Ошибка ${response.status}: ${errorText}`);
        }

        return await response.json();
    } catch (error) {
        console.error("Ошибка при загрузке вопросов:", error);
        throw new Error(`Не удалось загрузить вопросы: ${error.message}`);
    }
};

export const submitAnswers = async (answers) => {
    try {
        const response = await fetch(`${API_BASE_URL}/test/evaluate`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(answers)
        });

        if (!response.ok) {
            throw new Error(`Ошибка HTTP: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error("Ошибка при отправке ответов:", error);
        throw error;
    }
};

export const submitDecision = async (answers) => {
    try {
        const response = await fetch(`${API_BASE_URL}/test/execute`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(answers)
        });

        if (!response.ok) {
            throw new Error(`Ошибка HTTP: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error("Ошибка при отправке ответов:", error);
        throw error;
    }
};