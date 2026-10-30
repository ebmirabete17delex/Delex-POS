import { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { ActivationRequest, AuthenticationClient } from '../../web-api-client.ts';
import { createApiClient, storeAccessToken, storeUserId } from '../api-client.jsx';
import { notify } from '../toast.jsx';

const authentication = createApiClient(AuthenticationClient);

export function ActivationPage() {
  const location = useLocation();
  const [form, setForm] = useState({
    userId: location.state?.userId ?? '',
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
  });
  const [busy, setBusy] = useState(false);
  const navigate = useNavigate();

  async function submit(event) {
    event.preventDefault();
    setBusy(true);
    try {
      if (form.newPassword !== form.confirmPassword) {
        throw new Error('New password and confirmation do not match.');
      }
      const response = await authentication.activate(new ActivationRequest({
        userId: form.userId,
        oldPassword: form.oldPassword,
        newPassword: form.newPassword,
      }));
      if (!response?.successful) throw new Error(response?.error);
      storeAccessToken(response.accessToken);
      storeUserId(form.userId);
      navigate('/', { replace: true });
      notify.success('Activation Successful!');
    } catch (requestError) {
      notify.error(requestError?.message || 'Unable to activate this account.');
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
      <p>Enter your current password and set a new password.</p>
      <form onSubmit={submit}>
        <label htmlFor="activate-user-id">User ID</label>
        <input id="activate-user-id" value={form.userId} required
          onChange={event => update('userId', event.target.value)} />
        <label htmlFor="activate-old-password">Current password</label>
        <input id="activate-old-password" type="password" value={form.oldPassword} required
          onChange={event => update('oldPassword', event.target.value)} />
        <label htmlFor="activate-new-password">New password</label>
        <input id="activate-new-password" type="password" value={form.newPassword} required
          onChange={event => update('newPassword', event.target.value)} />
        <label htmlFor="activate-confirm-password">Confirm new password</label>
        <input id="activate-confirm-password" type="password" value={form.confirmPassword} required
          onChange={event => update('confirmPassword', event.target.value)} />
        <button type="submit" disabled={busy}>{busy ? 'Activating...' : 'Activate account'}</button>
      </form>
    </article>
  );
}
