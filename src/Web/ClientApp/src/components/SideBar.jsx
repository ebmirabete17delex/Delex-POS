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
import { createApiClient, getUserId } from './api-client.jsx';
import { useCallback, useState, useEffect } from 'react';

const usersClient = createApiClient(UsersClient);

export const navigationGroups = [
  {
    label: 'Sales',
    items: [
      { label: 'Point-of-Sale', to: '/point-of-sale', icon: Package },
      { label: 'X-Reading', to: '/sales-x-reading', icon: Layers3 },
      { label: 'Z-Reading', to: '/sales-z-reading', icon: Tags },
      { label: 'shift-closing', to: '/shift-closing', icon: BadgePercent },
      { label: 'Summary Reports', to: '/summary-report', icon: BadgePercent },
    ],
  },
  {
    label: 'Loyalty',
    items: [
      { label: 'Enroll Customer', to: '/loyalty-enroll', icon: Package },
      { label: 'Configurations', to: '/loyalty-configuration', icon: Layers3 },
      { label: 'Product Coverage', to: '/loyalty-coverage', icon: Tags },
    ],
  },
  {
    label: 'Inventory',
    items: [
      { label: 'Purchase Order', to: '/purchase-order', icon: Package },
      { label: 'Return To Seller', to: '/return-to-seller', icon: Layers3 },
      { label: 'Receive Delivery', to: '/receive-delivery', icon: Layers3 },
      { label: 'Low-stock / No-Stock', to: '/stock-lowno', icon: Layers3 },
      { label: 'Near expiry and expired', to: '/stock-expiry', icon: Layers3 },
    ],
  },
  {
    label: 'Transfer Products',
    items: [
      { label: 'Transfer Request', to: '/transfer-request', icon: Tags },
      { label: 'Transfer Approval', to: '/transfer-approval', icon: BadgePercent },
      { label: 'Stock Reservation', to: '/transfer-reservation', icon: BadgePercent },
      { label: 'Picking and Packing', to: '/transfer-pick-pack', icon: FlaskConical },
      { label: 'Dispatch Items', to: '/transfer-dispatch', icon: FlaskConical },
      { label: 'Receive Transfer', to: '/transer-receive', icon: Ruler },
      { label: 'Archive - Approved Transfers', to: '/archive-approval', icon: Ruler },
      { label: 'Archive - Tranfer Requests', to: '/archive-transfer', icon: Ruler },
      { label: 'Archive - Deliveries', to: '/archive-delivery', icon: Ruler },
    ],
  },
  {
    label: 'Reports',
    items: [
      { label: 'Purchase Order', to: '/purchase-order', icon: Package },
      { label: 'Return To Seller', to: '/return-to-seller', icon: Layers3 },
      { label: 'Receive Delivery', to: '/receive-delivery', icon: Layers3 },
      { label: 'Transfer Request', to: '/transfer-request', icon: Tags },
      { label: 'Transfer Approval', to: '/transfer-approval', icon: BadgePercent },
      { label: 'Stock Reservation', to: '/transfer-reservation', icon: BadgePercent },
      { label: 'Picking and Packing', to: '/transfer-pick-pack', icon: FlaskConical },
      { label: 'Dispatch Items', to: '/transfer-dispatch', icon: FlaskConical },
      { label: 'Receive Transfer', to: '/transer-receive', icon: Ruler },
      { label: 'Archive - Approved Transfers', to: '/archive-approval', icon: Ruler },
      { label: 'Archive - Tranfer Requests', to: '/archive-transfer', icon: Ruler },
      { label: 'Archive - Deliveries', to: '/archive-delivery', icon: Ruler },
    ],
  },
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
  const [features, setFeatures] = useState([]);
  const [sideBarNavGroups, setSideBarNavGroups] = useState([]);
  const [loading, setLoading] = useState(true);
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState('');
  const id = getUserId();
  
  const setMenu = useCallback(async () =>{
    if (!id){
      setError('A user ID is required to Display access.');
      setLoading(false);
      return;
    }    
    setLoading(true);
    setError('');
    try{
      const [userFeatures] = await Promise.all([
        usersClient.getUserFeatures(id),
      ]);
      setFeatures(userFeatures ?? []);
    } catch (requestError) {
      setError(requestError?.message || 'Could not load User access assignments.');
    } finally {
      setLoading(false);
    }
  }, [id])

  useEffect(() => { setMenu(); }, [setMenu]);

  useEffect(() => { SetNavBarItems();}, [features]);

  function SetNavBarItems(){
    let tempNavGroup = [];
    navigationGroups.map(group => {
      const accessFeatures = group.items.filter(a => features.some(i => i.feature == a.to.replace('/',''))); 
      if (accessFeatures.length > 0){
        group.items.splice(0, group.items.length, ...accessFeatures);
        tempNavGroup.push(group);
      }
    });
    setSideBarNavGroups(tempNavGroup);
  }
  
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
        {sideBarNavGroups.map(group => ( 
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
