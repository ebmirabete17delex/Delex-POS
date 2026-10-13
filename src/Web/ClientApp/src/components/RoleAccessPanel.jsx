import { useCallback, useEffect, useState } from 'react';
import { AccessClient, RoleClient } from '../web-api-client.ts';
import { createApiClient } from './api-client.js';

const roleClient = createApiClient(RoleClient);
const accessClient = createApiClient(AccessClient);
const permissions = [
  ['hasRead', 'Read'],
  ['hasWrite', 'Write'],
  ['hasUpdate', 'Update'],
  ['hasDelete', 'Delete'],
  ['isActive', 'Active'],
];

export default function RoleAccessPanel({ roleId }) {
  const [assignments, setAssignments] = useState([]);
  const [available, setAvailable] = useState([]);
  const [selectedAccessId, setSelectedAccessId] = useState('');
  const [loading, setLoading] = useState(true);
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState('');

  const refresh = useCallback(async () => {
    if (!roleId) {
      setError('A role ID is required to manage access.');
      setLoading(false);
      return;
    }
    setLoading(true);
    setError('');
    try {
      const [roleAccess, accessClaims] = await Promise.all([
        roleClient.getRoleAccess(roleId),
        accessClient.getAccessClaimsList(),
      ]);
      setAssignments(roleAccess ?? []);
      setAvailable(accessClaims ?? []);
    } catch (requestError) {
      setError(requestError?.message || 'Could not load role access assignments.');
    } finally {
      setLoading(false);
    }
  }, [roleId]);

  useEffect(() => { refresh(); }, [refresh]);

  async function addAccess(event) {
    event.preventDefault();
    setBusy(true);
    setError('');
    try {
      await roleClient.addRoleAccess(roleId, Number(selectedAccessId));
      setSelectedAccessId('');
      await refresh();
    } catch (requestError) {
      setError(requestError?.message || 'Could not assign the access claim to this role.');
    } finally {
      setBusy(false);
    }
  }

  async function changePermission(assignment, permission, value) {
    setBusy(true);
    setError('');
    try {
      await roleClient.updateRoleAccess(roleId, assignment.accessId, {
        roleId,
        accessId: assignment.accessId,
        hasRead: permission === 'hasRead' ? value : Boolean(assignment.hasRead),
        hasWrite: permission === 'hasWrite' ? value : Boolean(assignment.hasWrite),
        hasUpdate: permission === 'hasUpdate' ? value : Boolean(assignment.hasUpdate),
        hasDelete: permission === 'hasDelete' ? value : Boolean(assignment.hasDelete),
        isActive: permission === 'isActive' ? value : Boolean(assignment.isActive),
      });
      await refresh();
    } catch (requestError) {
      setError(requestError?.message || 'Could not update role permissions.');
    } finally {
      setBusy(false);
    }
  }

  async function removeAccess(accessId) {
    setBusy(true);
    setError('');
    try {
      await roleClient.removeRoleAccess(roleId, accessId);
      await refresh();
    } catch (requestError) {
      setError(requestError?.message || 'Could not remove this access claim.');
    } finally {
      setBusy(false);
    }
  }

  return (
    <section className="admin-section">
      <header className="entity-toolbar">
        <div>
          <h2>Access permissions</h2>
          <p>Role ID: {roleId}</p>
        </div>
        <button type="button" className="secondary" onClick={refresh} disabled={loading}>Refresh</button>
      </header>
      {error && <p role="alert" className="error">{error}</p>}
      <form className="entity-search" onSubmit={addAccess}>
        <select aria-label="Access claim" value={selectedAccessId} required
          onChange={event => setSelectedAccessId(event.target.value)}>
          <option value="">Choose an access claim</option>
          {available
            .filter(item => !assignments.some(assignment => assignment.accessId === item.id))
            .map(item => <option key={item.id} value={item.id}>{item.name}</option>)}
        </select>
        <button type="submit" disabled={busy || loading}>Assign claim</button>
      </form>
      {loading ? <p aria-busy="true">Loading role permissions...</p> : (
        <div className="admin-table-wrap">
          <table className="admin-table">
            <thead>
              <tr>
                <th>Access claim</th>
                {permissions.map(([, label]) => <th key={label}>{label}</th>)}
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {assignments.map(assignment => (
                <tr key={assignment.accessId}>
                  <th scope="row">{assignment.name}</th>
                  {permissions.map(([key, label]) => (
                    <td key={key}>
                      <input type="checkbox" aria-label={`${label} for ${assignment.name}`}
                        checked={Boolean(assignment[key])} disabled={busy}
                        onChange={event => changePermission(assignment, key, event.target.checked)} />
                    </td>
                  ))}
                  <td>
                    <button type="button" className="contrast" disabled={busy}
                      onClick={() => removeAccess(assignment.accessId)}>Remove</button>
                  </td>
                </tr>
              ))}
              {assignments.length === 0 && (
                <tr><td colSpan={permissions.length + 2}>No access claims assigned to this role.</td></tr>
              )}
            </tbody>
          </table>
        </div>
      )}
    </section>
  );
}
