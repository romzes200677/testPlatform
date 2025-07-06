import React, { useEffect, useRef } from 'react';
import * as monaco from 'monaco-editor';
import styles from './CodeEditor.module.css';

const CodeEditor = ({ template, userCode, onCodeChange, language = 'csharp' }) => {
  const editorRef = useRef(null);
  const editorInstance = useRef(null);
  const placeholder = '###';
  
  // Формируем полный код с заменой плейсхолдера
  const fullCode = template.replace(placeholder, userCode);

  useEffect(() => {
    if (editorRef.current) {
      editorInstance.current = monaco.editor.create(editorRef.current, {
        value: fullCode,
        language: language,
        minimap: { enabled: false },
        automaticLayout: true,
        scrollBeyondLastLine: false,
        theme: 'vs-dark',
        fontSize: 14,
        lineNumbers: 'on',
        roundedSelection: false,
        readOnly: false,
      });
      
      editorInstance.current.onDidChangeModelContent(() => {
        const currentValue = editorInstance.current.getValue();
        
        // Извлекаем пользовательский код между плейсхолдерами
        const startIndex = currentValue.indexOf(placeholder);
        if (startIndex !== -1) {
          const endIndex = currentValue.indexOf(placeholder, startIndex + placeholder.length);
          if (endIndex !== -1) {
            const newUserCode = currentValue.substring(
              startIndex + placeholder.length,
              endIndex
            );
            onCodeChange(newUserCode);
          }
        }
      });
    }

    return () => {
      if (editorInstance.current) {
        editorInstance.current.dispose();
      }
    };
  }, [language]);

  useEffect(() => {
    if (editorInstance.current && fullCode !== editorInstance.current.getValue()) {
      editorInstance.current.setValue(fullCode);
    }
  }, [fullCode]);

  return <div ref={editorRef} className={styles.editorContainer} />;
};

export default CodeEditor;