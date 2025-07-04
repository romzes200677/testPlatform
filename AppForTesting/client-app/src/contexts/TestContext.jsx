import { createContext, useContext, useState } from 'react';

// Создаем контекст
const TestContext = createContext();

// Провайдер контекста
export function TestProvider({ children }) {
    const [testResult, setTestResult] = useState(null);

    return (
        <TestContext.Provider value={{ testResult, setTestResult }}>
            {children}
        </TestContext.Provider>
    );
}

// Хук для использования контекста
export const useTestContext = () => {
    const context = useContext(TestContext);
    if (!context) {
        throw new Error("useTestContext must be used within a TestProvider");
    }
    return context;
};