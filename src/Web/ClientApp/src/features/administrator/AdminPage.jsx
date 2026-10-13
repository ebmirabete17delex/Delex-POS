import { useState } from 'react';
import { Shield, UserRound } from 'lucide-react';
import {
  AccessPanel,
  BranchPanel,
  RegionPanel,
  RolePanel,
  UsersPanel,
} from '../../components/business-pages.jsx';

export function AdminPage() {
  const [activeTab, setActiveTab] = useState('users');
  const tabs = [
    { id: 'users', label: 'Users', icon: UserRound },
    { id: 'roles', label: 'Roles', icon: Shield },
    { id: 'access', label: 'Access claims', icon: Shield },
    { id: 'branches', label: 'Branches', icon: Shield },
    { id: 'regions', label: 'Regions', icon: Shield },
  ];

  return <div className="admin-page">
    <header className="admin-hero">
      <div>
        <p className="eyebrow">Operations</p>
        <h1>Administration</h1>
        <p>Manage the people, permissions, and locations behind Delex POS.</p>
      </div>
    </header>
    <nav className="admin-tabs" aria-label="Administration sections">
      {tabs.map(({ id, label, icon: Icon }) => (
        <button 
          key={id} 
          type="button" 
          className={activeTab === id ? 'active' : ''} 
          onClick={() => setActiveTab(id)}>
          <Icon size={17} />{label}
        </button>
      ))}
    </nav>
    {activeTab === 'users' && <UsersPanel />}
    {activeTab === 'roles' && <RolePanel />}
    {activeTab === 'access' && <AccessPanel />}
    {activeTab === 'branches' && <BranchPanel />}
    {activeTab === 'regions' && <RegionPanel />}
  </div>;
}