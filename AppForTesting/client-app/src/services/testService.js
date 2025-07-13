const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ||'http://localhost:5231/api/test';

export const fetchQuestions = async (pageNumber, pageSize) => {
  try {
    const response = await fetch(`${API_BASE_URL}/questions?PageNumber=${pageNumber}&PageSize=${pageSize}`);

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Ошибка ${response.status}: ${errorText}`);
    }

    return await response.json(); // Return the JSON data instead of setQuestions
  } catch (error) {
    console.error('Ошибка при загрузке вопросов:', error);
    throw error; // Re-throw the error for the component to handle
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