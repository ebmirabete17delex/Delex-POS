import { useCallback, useEffect, useState } from "react";
import { AccessClient } from "../../web-api-client";
import { createApiClient } from "../api-client";

const accessClient = createApiClient(AccessClient);

export default function AccessPermissionPanel({ accessId }){
    const [newPermission, setNewPermission] = useState('');
    const [loading, setLoading] = useState(false);
    const [busy, setBusy] = useState(false);
    const [error, setError] = useState('');
    const [access, setAccess] = useState({});
    const [permissions, setPermissions] = useState([]);

    const refresh = useCallback(async () => {
        if (!accessId){
            setError('An Access Id is required to manage access permissions');
            setLoading(false);
            return;
        }
        setLoading(true);
        setError('');
        try{
            const [access, permissions] = await Promise.all([
                accessClient.getAccessClaim(accessId),
                accessClient.getAccessPermissions(accessId)
            ]);
            setAccess(access ?? {});
            setPermissions(permissions ?? []);
        } catch (requestError){
            setError(requestError?.message || 'Could not update role permissions.');
        } finally {
            setLoading(false);
        }
    }, [accessId]);

    useEffect(() => { refresh(); }, [refresh]);

    async function addPermission(event){
        event.preventDefault();
        setBusy(true);
        setError('');
        try{
            await accessClient.createAccessPermission(accessId, 
                {
                    accessId: accessId, 
                    name: newPermission
                });
            setNewPermission('');
            await refresh();
        } catch (requestError) {
            setError(requestError?.message || 'Could not add the permission to this access.');
        } finally {
            setBusy(false);
        }
    }

    async function removePermission(keyId){
        setBusy(true);
        setError('');
        try{
            await accessClient.deleteAccessPermission(keyId, accessId);
            setNewPermission('');
            await refresh();
        } catch (requestError) {
            setError(requestError?.message || 'Could not remove this permission.');
        } finally {
            setBusy(false);
        }
    }

    return(
        <section className="admin-section entity-panel">
            <header className="entity-toolbar">
                <div>
                <h2>Permissions</h2>
                <p>Access: {access.name ?? ''}</p>
                </div>
                <button type="button" className="secondary" onClick={refresh} disabled={loading} >Refresh</button>
            </header>
            {error && <p role="alert" className="error">{error}</p>}
            <form className="entity-serch" onSubmit={addPermission}>
                <input type="text" 
                    required 
                    value={newPermission} 
                    onChange={event => setNewPermission(event.target.value)} />
                <button type="submit" disabled={busy || loading}>Add Permission</button>
            </form>
            {loading ? <p aria-busy="true">Loading permissions...</p> : (
                <div className="admin-table-wrap">
                    <table className="admin-table">
                        <thead>
                            <tr>
                                <th>Permission</th>
                                <th>Actions</th>
                            </tr>                            
                        </thead>
                        <tbody>
                            {permissions.map((perm) => (
                                <tr key={perm.id}>
                                    <td>{perm.name}</td>
                                    <td>
                                        <button type="button" className="contrast" disabled={busy}
                                            onClick={() => removePermission(perm.id)}>Remove</button>
                                    </td>
                                </tr>                                
                            ))}
                            {permissions.length === 0 && (
                                <tr><td colSpan={permissions.length + 2}>No Permissions defined to this Access.</td></tr>
                            )}
                        </tbody>
                    </table>
                </div>
            )}
        </section>
    );
}