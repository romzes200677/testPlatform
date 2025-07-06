import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { loader } from '@monaco-editor/react';

// Настройка путей для Monaco Editor
loader.config({ 
  paths: { 
    vs: 'https://cdn.jsdelivr.net/npm/monaco-editor@0.34.0/min/vs' 
  }
});

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);