import { ArrowLeft } from 'lucide-react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import UserAccessPanel from './UserAccessPanel.jsx';

export function UserAccessPage() {
  const { userId } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const roleName = location.state?.userName;

  return (
    <div className="admin-page">
      <header className="admin-hero">
        <div>
          <button
            className="secondary"
            type="button"
            onClick={() => navigate('/users')}
          >
            <ArrowLeft size={17} />
            Back to users
          </button>
          <p className="eyebrow">Administration</p>
          <h1>User access</h1>
          <p>{roleName || `User ID: ${userId}`}</p>
        </div>
      </header>
      <UserAccessPanel userId={userId} />
    </div>
  );
}
