import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { TestProvider } from './contexts/TestContext';
import TestPage from './pages/TestPage/TestPage';
import ResultsPage from './pages/ResultsPage/ResultsPage';
import CodeExecutionPage from './pages/CodeExecutionPage/CodeExecutionPage';


function App() {
    return (
        <TestProvider>
            <Router>
                <Routes>
                    <Route path="/" element={<TestPage />} />
                    <Route path="/results" element={<ResultsPage />} />
                    {/* Новый маршрут для страницы выполнения кода */}
                    <Route path="/assignment/:assignmentId" element={<CodeExecutionPage />} />
                    
                    {/* Пример маршрута для конкретного задания */}
                    <Route path="/math-assignment" element={
                      <CodeExecutionPage assignmentId="math-1" />
                    } />
                </Routes>
            </Router>
        </TestProvider>
    );
}

export default App;