import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ActivationRequest, AuthenticationClient } from '../../web-api-client.ts';
import { createApiClient, storeAccessToken } from '../api-client.js';

const authentication = createApiClient(AuthenticationClient);

export function ActivationPage() {
  const [form, setForm] = useState({ userId: '', password: '', pin: '' });
  const [error, setError] = useState('');
  const [busy, setBusy] = useState(false);
  const navigate = useNavigate();

  async function submit(event) {
    event.preventDefault();
    setBusy(true);
    setError('');
    try {
      const response = await authentication.activate(new ActivationRequest(form));
      if (!response?.accessToken) throw new Error('The activation response did not include an access token.');
      storeAccessToken(response.accessToken);
      navigate('/', { replace: true });
    } catch (requestError) {
      setError(requestError?.message || 'Unable to activate this account.');
    } finally {
      setBusy(false);
    }
  }

  function update(name, value) {
    setForm(current => ({ ...current, [name]: value }));
  }

  return (
    <article className="auth-page">
      <h1>Activate account</h1>
      <p>Enter your account details and activation PIN.</p>
      <form onSubmit={submit}>
        <label htmlFor="activate-user-id">User ID</label>
        <input id="activate-user-id" value={form.userId} required
          onChange={event => update('userId', event.target.value)} />
        <label htmlFor="activate-password">New password</label>
        <input id="activate-password" type="password" value={form.password} required
          onChange={event => update('password', event.target.value)} />
        <label htmlFor="activate-pin">Activation PIN</label>
        <input id="activate-pin" value={form.pin} required
          onChange={event => update('pin', event.target.value)} />
        {error && <p className="error" role="alert">{error}</p>}
        <button type="submit" disabled={busy}>{busy ? 'Activating...' : 'Activate account'}</button>
      </form>
    </article>
  );
}
