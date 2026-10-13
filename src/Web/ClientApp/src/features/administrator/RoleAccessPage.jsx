import { ArrowLeft } from 'lucide-react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import RoleAccessPanel from '../../components/RoleAccessPanel.jsx';

export function RoleAccessPage() {
  const { roleId } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const roleName = location.state?.roleName;

  return (
    <div className="admin-page">
      <header className="admin-hero">
        <div>
          <button
            className="secondary"
            type="button"
            onClick={() => navigate('/roles')}
          >
            <ArrowLeft size={17} />
            Back to roles
          </button>
          <p className="eyebrow">Administration</p>
          <h1>Role access</h1>
          <p>{roleName || `Role ID: ${roleId}`}</p>
        </div>
      </header>
      <RoleAccessPanel roleId={roleId} />
    </div>
  );
}
