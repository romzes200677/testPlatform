import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { TestProvider } from './contexts/TestContext';
import TestPage from './pages/TestPage/TestPage'; // Прямой импорт
import ResultsPage from './pages/ResultsPage/ResultsPage'; // Прямой импорт

function App() {
    return (
        <TestProvider>
            <Router>
                <Routes>
                    <Route path="/" element={<TestPage />} />
                    <Route path="/results" element={<ResultsPage />} />
                </Routes>
            </Router>
        </TestProvider>
    );
}

export default App;