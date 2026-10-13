import { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from './AuthContext.jsx';

export function LoginPage() {
  const [userId, setUserId] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [busy, setBusy] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setBusy(true);
    setError('');
    try {
      await login(userId, password);
      const returnUrl = location.state?.returnUrl || '/';
      navigate(returnUrl, { replace: true });
    } catch (requestError) {
      setError(requestError?.message || 'Unable to sign in. Check your credentials and try again.');
    } finally {
      setBusy(false);
    }
  };

  return (
    <article className="auth-page">
      <h1>Sign in</h1>
      <p>Sign in to access the Delex POS workspace.</p>
      <form onSubmit={handleSubmit}>
        <label htmlFor="userId">User ID</label>
        <input type="text" id="userId" autoComplete="username" required
          value={userId} onChange={event => setUserId(event.target.value)} />
        <label htmlFor="password">Password</label>
        <input type="password" id="password" autoComplete="current-password" required
          value={password} onChange={event => setPassword(event.target.value)} />
        {error && <p className="error" role="alert">{error}</p>}
        <button type="submit" disabled={busy}>{busy ? 'Signing in...' : 'Sign in'}</button>
      </form>
    </article>
  );
}
