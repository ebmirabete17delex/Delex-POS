import { createContext, useContext, useEffect, useState } from 'react';

const STORAGE_KEY = 'picoColorScheme';

const ThemeContext = createContext({});

export function useTheme() {
  return useContext(ThemeContext);
}

export function ThemeProvider({ children }) {
  const [theme, setTheme] = useState(
    () => localStorage.getItem(STORAGE_KEY) || 'auto'
  );

  useEffect(() => {
    const root = document.documentElement;
    const systemTheme = window.matchMedia('(prefers-color-scheme: dark)');
    const applyTheme = () => {
      const isDark = theme === 'dark' || (theme === 'auto' && systemTheme.matches);
      root.classList.toggle('dark', isDark);

      if (theme === 'auto') {
        root.removeAttribute('data-theme');
      } else {
        root.setAttribute('data-theme', theme);
      }
    };

    applyTheme();
    localStorage.setItem(STORAGE_KEY, theme);

    if (theme === 'auto') {
      systemTheme.addEventListener('change', applyTheme);
      return () => systemTheme.removeEventListener('change', applyTheme);
    }
  }, [theme]);

  return (
    <ThemeContext.Provider value={{ theme, setTheme }}>
      {children}
    </ThemeContext.Provider>
  );
}
