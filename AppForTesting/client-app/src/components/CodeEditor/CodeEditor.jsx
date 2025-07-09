import React, { useEffect, useRef } from 'react';
import * as monaco from 'monaco-editor';
import styles from './CodeEditor.module.css';

const CodeEditor = ({ template, userCode, onCodeChange, language = 'csharp' }) => {
  const editorRef = useRef(null);
  const editorInstance = useRef(null);
  
  // Формируем полный код с заменой плейсхолдера
  const fullCode = template.replace('###', userCode);

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
      
      // Обработчик изменений кода
      const changeListener = editorInstance.current.onDidChangeModelContent(() => {
        const currentValue = editorInstance.current.getValue();
        
        // Извлекаем пользовательский код после плейсхолдера
        const placeholderIndex = template.indexOf('###');
        if (placeholderIndex !== -1) {
          const beforePlaceholder = template.substring(0, placeholderIndex);
          const afterPlaceholder = template.substring(placeholderIndex + 3);
          
          const startIndex = currentValue.indexOf(beforePlaceholder) + beforePlaceholder.length;
          const endIndex = currentValue.indexOf(afterPlaceholder, startIndex);
          
          if (startIndex !== -1 && endIndex !== -1) {
            const newUserCode = currentValue.substring(startIndex, endIndex);
            onCodeChange(newUserCode);
          }
        }
      });

      // Сохраняем ссылку на листенер для очистки
      return () => {
        changeListener.dispose();
        if (editorInstance.current) {
          editorInstance.current.dispose();
        }
      };
    }
  }, [language]);

  useEffect(() => {
    if (editorInstance.current) {
      const currentValue = editorInstance.current.getValue();
      if (fullCode !== currentValue) {
        // Сохраняем позицию курсора
        const position = editorInstance.current.getPosition();
        
        editorInstance.current.setValue(fullCode);
        
        // Восстанавливаем позицию курсора
        if (position) {
          editorInstance.current.setPosition(position);
          editorInstance.current.revealPositionInCenter(position);
        }
      }
    }
  }, [fullCode]);

  return <div ref={editorRef} className={styles.editorContainer} />;
};

export default CodeEditor;