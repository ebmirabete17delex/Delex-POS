import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const currentTheme = () => {
  const root = document.documentElement;
  const stored = root.getAttribute('data-theme');
  if (stored === 'dark' || stored === 'light') return stored;
  return root.classList.contains('dark') ? 'dark' : 'light';
};

const themeColors = {
  light: {
    success: { bg: '#ecfdf5', text: '#065f46', border: '#86efac' },
    error: { bg: '#fef2f2', text: '#991b1b', border: '#fecaca' },
    warn: { bg: '#fffbeb', text: '#92400e', border: '#fde047' },
    info: { bg: '#eff6ff', text: '#1d4ed8', border: '#bfdbfe' },
  },
  dark: {
    success: { bg: '#064e3b', text: '#bbf7d0', border: '#16a34a' },
    error: { bg: '#7f1d1d', text: '#fecaca', border: '#dc2626' },
    warn: { bg: '#713f12', text: '#fef3c7', border: '#d97706' },
    info: { bg: '#1e3a8a', text: '#bfdbfe', border: '#2563eb' },
  },
};

const optionsFor = (type) => {
  const theme = currentTheme();
  const c = themeColors[theme][type];
  return {
    autoClose: type === 'error' || type === 'warn' ? 5000 : type === 'success' ? 3000 : 4000,
    style: {
      background: c.bg,
      color: c.text,
      border: `1px solid ${c.border}`,
    },
  };
};

export const notify = {
  success: (message, options) => toast.success(message, { ...optionsFor('success'), ...options }),
  error: (message, options) => toast.error(message, { ...optionsFor('error'), ...options }),
  warn: (message, options) => toast.warn(message, { ...optionsFor('warn'), ...options }),
  info: (message, options) => toast.info(message, { ...optionsFor('info'), ...options }),
};
