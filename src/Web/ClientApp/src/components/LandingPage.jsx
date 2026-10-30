import { useCallback, useEffect, useState } from 'react';
import { NavLink, useLocation } from 'react-router-dom';
import { UsersClient } from '../web-api-client.ts';
import {
  Archive,
  BadgePercent,
  Boxes,
  Building2,
  ClipboardList,
  ContactRound,
  FileKey2,
  FlaskConical,
  Layers3,
  MapPinned,
  Package,
  Ruler,
  Shield,
  Tags,
  Truck,
  UsersRound,
  Warehouse,
} from 'lucide-react';
import { createApiClient, getUserId } from './api-client.jsx';
import { navigationGroups } from './SideBar.jsx';

const usersClient = createApiClient(UsersClient);

export function LandingPage() {
  const location = useLocation();
  const [features, setFeatures] = useState([]);
  const [navGroups, setNavGroups] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const id = getUserId();

  const loadFeatures = useCallback(async () => {
    if (!id) {
      setError('A user ID is required to display access.');
      setLoading(false);
      return;
    }
    setLoading(true);
    setError('');
    try {
      const [userFeatures] = await Promise.all([usersClient.getUserFeatures(id)]);
      setFeatures(userFeatures ?? []);
    } catch (requestError) {
      setError(requestError?.message || 'Could not load user access assignments.');
    } finally {
      setLoading(false);
    }
  }, [id]);

  useEffect(() => { loadFeatures(); }, [loadFeatures]);

  useEffect(() => {
    const tempNavGroup = [];
    navigationGroups.forEach((group) => {
      const accessFeatures = group.items.filter((item) =>
        features.some((f) => f.feature === item.to.replace('/', ''))
      );
      if (accessFeatures.length > 0) {
        tempNavGroup.push({ ...group, items: accessFeatures });
      }
    });
    setNavGroups(tempNavGroup);
  }, [features]);

  if (loading) {
    return (
      <div className="admin-page">
        <header className="admin-hero">
          <div>
            <p className="eyebrow">Delex POS</p>
            <h1>Workspace</h1>
          </div>
        </header>
        <p aria-live="polite">Loading workspace...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="admin-page">
        <header className="admin-hero">
          <div>
            <p className="eyebrow">Delex POS</p>
            <h1>Workspace</h1>
          </div>
        </header>
        <p className="error" role="alert">{error}</p>
      </div>
    );
  }

  return (
    <div className="admin-page">
      <header className="admin-hero">
        <div>
          <p className="eyebrow">Delex POS</p>
          <h1>Workspace</h1>
          <p>Manage your business operations from a single place.</p>
        </div>
      </header>
      <nav className="admin-tabs" aria-label="Quick navigation">
        {navGroups.map((group) => (
          <div key={group.label} className="sidebar-group">
            <h3 className="sidebar-group-label">{group.label}</h3>
            <div className="sidebar-group-links">
              {group.items.map((item) => {
                const Icon = item.icon;
                const active =
                  location.pathname === item.to ||
                  (item.to === '/roles' && location.pathname.startsWith('/roles/'));
                return (
                  <NavLink
                    key={item.to}
                    to={item.to}
                    className={({ isActive }) => (isActive ? 'active' : '')}
                    end={item.to === '/'}
                  >
                    <Icon aria-hidden="true" />
                    <span>{item.label}</span>
                  </NavLink>
                );
              })}
            </div>
          </div>
        ))}
        <div className="sidebar-group">
          <NavLink to="/" end className={({ isActive }) => (isActive ? 'active' : '')}>
            <Boxes aria-hidden="true" />
            <span>Workspace home</span>
          </NavLink>
        </div>
      </nav>
      <main className="dashboard-content">
        <section className="dashboard-feature">
          <h2>Welcome to Delex POS</h2>
          <p>Select a section from the navigation above to begin managing your records.</p>
        </section>
      </main>
    </div>
  );
}
