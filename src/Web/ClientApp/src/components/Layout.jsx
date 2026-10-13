import { NavMenu } from './NavMenu.jsx';
import { SideBar } from './SideBar.jsx';
import { SidebarInset, SidebarProvider } from './ui/sidebar.tsx';
import { useAuth } from './api-authorization/AuthContext.jsx';

export function Layout({ children }) {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return (
      <>
        <NavMenu />
        <main>{children}</main>
      </>
    );
  }

  return (
    <SidebarProvider className="application-shell">
      <NavMenu />
      <div className="app-sidebar-layout">
          <SideBar />
          <SidebarInset className="app-sidebar-main">
            <div className="app-page-content">{children}</div>
          </SidebarInset>
      </div>
    </SidebarProvider>
  );
}
