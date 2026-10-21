import { useCallback, useEffect, useState } from 'react';
import { AccessClient, RoleClient } from '../web-api-client.ts';
import { createApiClient } from './api-client.js';

const roleClient = createApiClient(RoleClient);
const accessClient = createApiClient(AccessClient);

export default function RoleAccessPanel({ roleId }) {
  const [assignments, setAssignments] = useState([]);
  const [available, setAvailable] = useState([]);
  const [selectedAccessId, setSelectedAccessId] = useState('');
  const [loading, setLoading] = useState(true);
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState('');
  const [roleName, setRoleName] = useState(''); 

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
        roleClient.getRoleAccessPermissions(roleId),
        accessClient.getAccessClaimsList()
      ]);
      console.log(roleAccess);
      setRoleName(roleClient.roleName ?? '');
      setAssignments(roleAccess ?? []);
      setAvailable(accessClaims ?? []);
      console.log('assignments: ' + assignments);
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

  async function changePermission(permissionId, value) {
    setBusy(true);
    setError('');
    console.log('roleId: ' + roleId);
    try {
      if(value === true){
        await roleClient.createRolePermission({
          roleId: String(roleId),
          permissionId: Number(permissionId)
        });
      }else{
        await roleClient.deleteRolePermission(
          roleId,
          permissionId 
        );
      }
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
          <p>Role: {roleName}</p>
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
                <th>Permissions</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {assignments.length === 0 && 
                <tr><td colSpan={3}>No access claims assigned to this role.</td></tr>}
              {assignments.length !== 0 && assignments.map(assignment => (
                <tr key={assignment.accessId}>
                  <th scope="row">{assignment.name}</th>
                  {assignment.permissions.length === 0 ? <p aria-busy="true">No Set permissions</p> :                  
                  <td>
                    {assignment.permissions.map(perm => (
                      <label key={perm.permissionId}>
                        <input type="checkbox"
                          aria-label={`${perm.name} for ${assignment.name}`}
                          checked={Boolean(perm.rolePermissionId !== null)} disabled={busy}
                          onChange={event => changePermission(perm.permissionId, event.target.checked)}
                          />
                        {perm.name}
                      </label>
                    ))}
                  </td>
                  }
                  <td>
                    <button type="button" className="contrast" disabled={busy}
                      onClick={() => removeAccess(assignment.accessId)}>Remove</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </section>
  );
}
