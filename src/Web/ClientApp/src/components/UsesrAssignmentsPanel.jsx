import {
  AccessClient,
  BranchClient,
  PsgcClient,
  RoleClient,
  UsersClient,
} from '../web-api-client.ts';

export function UserAssignmentsPanel() {
  const [userId, setUserId] = useState('');
  const [accesses, setAccesses] = useState([]);
  const [roles, setRoles] = useState([]);
  const [availableAccesses, setAvailableAccesses] = useState([]);
  const [selectedAccessId, setSelectedAccessId] = useState('');
  const [newRoleId, setNewRoleId] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [busy, setBusy] = useState(false);

  async function loadAssignments(id) {
    if (!id) return;
    setLoading(true);
    setError('');
    try {
      const numericId = Number(id);
      const [userAccesses, userRoles, accessList] = await Promise.all([
        api(UsersClient).getUserAccess(numericId),
        api(UsersClient).getUserRoles(numericId),
        api(AccessClient).getAccessClaimsList(),
      ]);
      setAccesses(userAccesses ?? []);
      setRoles(userRoles ?? []);
      setAvailableAccesses(accessList ?? []);
    } catch (requestError) {
      setError(requestError?.message || "Could not load this user's access assignments.");
    } finally {
      setLoading(false);
    }
  }

  async function submitUser(event) {
    event.preventDefault();
    await loadAssignments(userId);
  }

  async function addAccess(event) {
    event.preventDefault();
    setBusy(true);
    try {
      await api(UsersClient).addUserAccess(Number(userId), Number(selectedAccessId));
      setSelectedAccessId('');
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not assign the access claim.');
    } finally {
      setBusy(false);
    }
  }

  async function changePermission(access, permission, value) {
    setBusy(true);
    try {
      await api(UsersClient).updateUserAccess(Number(userId), access.accessId, {
        userId: Number(userId),
        accessId: access.accessId,
        hasRead: permission === 'hasRead' ? value : Boolean(access.hasRead),
        hasWrite: permission === 'hasWrite' ? value : Boolean(access.hasWrite),
        hasUpdate: permission === 'hasUpdate' ? value : Boolean(access.hasUpdate),
        hasDelete: permission === 'hasDelete' ? value : Boolean(access.hasDelete),
        isActive: permission === 'isActive' ? value : Boolean(access.isActive),
      });
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not update the access claim.');
    } finally {
      setBusy(false);
    }
  }

  async function removeAccess(accessId) {
    setBusy(true);
    try {
      await api(UsersClient).removeUserAccess(Number(userId), accessId);
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not remove the access claim.');
    } finally {
      setBusy(false);
    }
  }

  async function assignRole(event) {
    event.preventDefault();
    setBusy(true);
    try {
      await api(UsersClient).assignUserRole(Number(userId), newRoleId);
      setNewRoleId('');
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not assign the role.');
    } finally {
      setBusy(false);
    }
  }

  async function removeRole(roleId) {
    setBusy(true);
    try {
      await api(UsersClient).dismissUserRole(Number(userId), roleId);
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not remove the role.');
    } finally {
      setBusy(false);
    }
  }

  return (
    <section className="admin-section user-assignments">
      <header className="entity-toolbar">
        <div>
          <h2>User access and roles</h2>
          <p>Manage access claims and roles assigned to an employee account.</p>
        </div>
      </header>
      <form className="entity-search" onSubmit={submitUser}>
        <label htmlFor="assignment-user-id">User ID</label>
        <input id="assignment-user-id" type="number" min="1" value={userId} required
          onChange={event => setUserId(event.target.value)} />
        <button type="submit" disabled={loading}>Load assignments</button>
      </form>
      {error && <p className="error" role="alert">{error}</p>}
      {loading && <p aria-busy="true">Loading assignments...</p>}
      {userId && !loading && (
        <>
          <form className="entity-search" onSubmit={addAccess}>
            <select aria-label="Access claim" value={selectedAccessId} required
              onChange={event => setSelectedAccessId(event.target.value)}>
              <option value="">Choose an access claim</option>
              {availableAccesses
                .filter(item => !accesses.some(access => access.accessId === item.id))
                .map(item => <option key={item.id} value={item.id}>{item.name}</option>)}
            </select>
            <button type="submit" disabled={busy}>Assign access</button>
          </form>
          <div className="admin-table-wrap">
            <table className="admin-table">
              <thead><tr><th>Access claim</th><th>Read</th><th>Write</th><th>Update</th><th>Delete</th><th>Active</th><th /></tr></thead>
              <tbody>
                {accesses.map(access => (
                  <tr key={access.accessId}>
                    <th scope="row">{access.name}</th>
                    {['hasRead', 'hasWrite', 'hasUpdate', 'hasDelete', 'isActive'].map(permission => (
                      <td key={permission}>
                        <input
                          type="checkbox"
                          aria-label={`${permission} for ${access.name}`}
                          checked={Boolean(access[permission])}
                          disabled={busy}
                          onChange={event => changePermission(access, permission, event.target.checked)}
                        />
                      </td>
                    ))}
                    <td><button type="button" className="contrast" disabled={busy}
                      onClick={() => removeAccess(access.accessId)}>Remove</button></td>
                  </tr>
                ))}
                {!accesses.length && <tr><td colSpan="7">No access claims assigned.</td></tr>}
              </tbody>
            </table>
          </div>
          <form className="entity-search" onSubmit={assignRole}>
            <input aria-label="Role ID" placeholder="Role ID" value={newRoleId} required
              onChange={event => setNewRoleId(event.target.value)} />
            <button type="submit" disabled={busy}>Assign role</button>
          </form>
          <ul className="assignment-list">
            {roles.map(role => (
              <li key={role.roleId}>
                <span>{role.roleName || role.roleId}</span>
                <button type="button" className="contrast" disabled={busy}
                  onClick={() => removeRole(role.roleId)}>Remove role</button>
              </li>
            ))}
            {!roles.length && <li>No roles assigned.</li>}
          </ul>
        </>
      )}
    </section>
  );
}
