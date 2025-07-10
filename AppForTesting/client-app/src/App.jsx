// App.jsx
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { TestProvider } from './contexts/TestContext';
import TestPage from './pages/TestPage/TestPage';
import ResultsPage from './pages/ResultsPage/ResultsPage';
import CodeExecutionPage from './pages/CodeExecutionPage/CodeExecutionPage';
import Navbar from './components/Navbar/Navbar';
import Sidebar from './components/Sidebar/Sidebar';
import styles from './App.module.css';

function App() {
  return (
    <TestProvider>
      <Router>
        <div className={styles.appContainer}>
          <Navbar />
          <div className={styles.mainContent}>
            <Sidebar />
            <div className={styles.contentArea}>
              <Routes>
                <Route path="/" element={<TestPage />} />
                <Route path="/results" element={<ResultsPage />} />
                <Route path="/assignment/:assignmentId" element={<CodeExecutionPage />} />
                <Route path="/math-assignment" element={<CodeExecutionPage assignmentId="math-1" />} />
              </Routes>
            </div>
          </div>
        </div>
      </Router>
    </TestProvider>
  );
}

export default App;