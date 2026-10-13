import { Link, useNavigate } from 'react-router-dom';
import { LogOut, Package } from 'lucide-react';
import { useAuth } from './api-authorization/AuthContext.jsx';
import { ThemeToggle } from './ThemeToggle.jsx';
import { SidebarTrigger } from './SideBar.jsx';

export function NavMenu() {
  const { isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();
  async function signOut() {
    try {
      await logout();
      navigate('/login', { replace: true });
    } catch (error) {
      console.error('Sign out failed.', error);
    }
  }

  return (
    <header>
      <nav aria-label="Main navigation">
        <ul className="top-nav-brand">
          {isAuthenticated && <li><SidebarTrigger /></li>}
          <li>
            <Link to="/" className="brand-link">
              <Package size={21} aria-hidden="true" />
              <span>Delex POS</span>
            </Link>
          </li>
        </ul>
        <ul className="top-nav-actions">
          {isAuthenticated ? (
            <>
              <li>
                <button type="button" className="nav-action-button" onClick={signOut} aria-label="Sign out">
                  <LogOut size={17} aria-hidden="true" />
                  <span>Sign out</span>
                </button>
              </li>
            </>
           ) : <li>
          {/* <Link to="/login">Sign in</Link> */}
           </li>}
          <li><ThemeToggle /></li>
        </ul>
      </nav>
    </header>
  );
}
