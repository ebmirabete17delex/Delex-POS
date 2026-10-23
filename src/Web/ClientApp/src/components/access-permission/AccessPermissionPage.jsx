import { ArrowLeft } from 'lucide-react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import AccessPermissionPanel from '../access-permission/AccessPermissionPanel.jsx';

export function AccessPermissionPage(){
    const { accessId } = useParams();
    const location = useLocation();
    const navigate = useNavigate();
    const accessName = location.state?.accessName;

    return(
        <div className="admin-page">
            <header className="admin-hero">
                <div>
                    <button
                        className="secondary"
                        type="button"
                        onClick={() => navigate('/access')}
                        >
                        <ArrowLeft size={17} />
                        Back to Access Claims
                    </button>
                    <p className='eyebrow'></p>
                    <h1>Access Permission</h1>
                    <p>{accessName || `Access ID: ${accessId}`}</p>
                </div>
            </header>
            <AccessPermissionPanel accessId={accessId} />
        </div>
    );
}