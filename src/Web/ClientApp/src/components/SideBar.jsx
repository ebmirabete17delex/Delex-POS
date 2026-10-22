import { NavLink, useLocation } from 'react-router-dom';
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
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarRail,
  SidebarTrigger,
} from './ui/sidebar.tsx';

const navigationGroups = [
  {
    label: 'Catalog',
    items: [
      { label: 'Products', to: '/products', icon: Package },
      { label: 'Product classes', to: '/product-classes', icon: Layers3 },
      { label: 'Categories', to: '/product-categories', icon: Tags },
      { label: 'Attributes', to: '/product-attributes', icon: BadgePercent },
      { label: 'Generics', to: '/generics', icon: FlaskConical },
      { label: 'Units of measure', to: '/units-of-measure', icon: Ruler },
    ],
  },
  {
    label: 'People',
    items: [
      { label: 'Customers', to: '/customers', icon: ContactRound },
      { label: 'Customer types', to: '/customer-types', icon: UsersRound },
      { label: 'Suppliers', to: '/suppliers', icon: Truck },
      { label: 'Users', to: '/users', icon: UsersRound },
    ],
  },
  {
    label: 'Locations',
    items: [
      { label: 'Warehouses', to: '/warehouses', icon: Warehouse },
      { label: 'Branches', to: '/branches', icon: Building2 },
    ],
  },
  {
    label: 'Administration',
    items: [
      { label: 'Roles', to: '/roles', icon: Shield },
      { label: 'Access claims', to: '/access', icon: FileKey2 },
      { label: 'Cancel reasons', to: '/cancel-reasons', icon: ClipboardList },
    ],
  },
];

export function SideBar() {
  const location = useLocation();

  return (
    <Sidebar collapsible="icon">
      <SidebarHeader>
        <div className="sidebar-brand">
          <span className="sidebar-brand-mark"><Package size={18} aria-hidden="true" /></span>
          <div>
            <strong>Delex Point-Of-Sale</strong>
            <small>Management workspace</small>
          </div>
        </div>
      </SidebarHeader>
      <SidebarContent>
        {navigationGroups.map(group => (
          <SidebarGroup key={group.label}>
            <SidebarGroupLabel>{group.label}</SidebarGroupLabel>
            <SidebarGroupContent>
              <SidebarMenu>
                {group.items.map(item => {
                  const Icon = item.icon;
                  const active = location.pathname === item.to
                    || (item.to === '/roles' && location.pathname.startsWith('/roles/'));
                  return (
                    <SidebarMenuItem key={item.to}>
                      <SidebarMenuButton
                        render={<NavLink to={item.to} />}
                        isActive={active}
                        tooltip={item.label}
                      >
                        <Icon aria-hidden="true" />
                        <span>{item.label}</span>
                      </SidebarMenuButton>
                    </SidebarMenuItem>
                  );
                })}
              </SidebarMenu>
            </SidebarGroupContent>
          </SidebarGroup>
        ))}
      </SidebarContent>
      <SidebarFooter>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton render={<NavLink to="/" />} isActive={location.pathname === '/'}>
              <Boxes aria-hidden="true" />
              <span>Workspace home</span>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
        <div className="sidebar-company">Delex Pharma International</div>
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  );
}

export { SidebarTrigger };
