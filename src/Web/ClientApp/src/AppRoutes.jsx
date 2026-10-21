import { ActivationPage } from './components/api-authorization/ActivationPage.jsx';
import { LoginPage } from './components/api-authorization/LoginPage.jsx';
import { ProtectedRoute } from './components/api-authorization/ProtectedRoute.jsx';
import {
  AccessPanel,
  BranchPanel,
  CancelReasonPanel,
  CustomerPanel,
  CustomerTypePanel,
  GenericPanel,
  ProductAttributePanel,
  ProductCategoryPanel,
  ProductClassPanel,
  ProductPanel,
  RegionPanel,
  RolePanel,
  SupplierPanel,
  UnitOfMeasurePanel,
  UsersPanel,
  WarehousePanel,
} from './components/business-pages.jsx';
import { AdminPage } from './features/administrator/AdminPage.jsx';
import { RoleAccessPage } from './features/administrator/RoleAccessPage.jsx';
import { AccessPermissionPage } from './features/administrator/AccessPermissionPage.jsx';
import { Navigate } from 'react-router-dom';

const protectedPage = element => <ProtectedRoute>{element}</ProtectedRoute>;
const AppRoutes = [
  { index: true, element: protectedPage(<Navigate to="/users" replace />) },
  { path: '/dashboard', element: protectedPage(<Navigate to="/users" replace />) },
  { path: '/admin', element: protectedPage(<AdminPage />) },
  { path: '/access', element: protectedPage(<AccessPanel />) },
  { path: '/access/:accessId/permission', element: protectedPage(<AccessPermissionPage />) },
  { path: '/roles', element: protectedPage(<RolePanel />) },
  { path: '/roles/:roleId/access', element: protectedPage(<RoleAccessPage />) },
  // { path: '/roles/:roleId/permission', element: protectedPage(<RolePermissionPage />) },
  { path: '/users', element: protectedPage(<UsersPanel />) },
  { path: '/branches', element: protectedPage(<BranchPanel />) },
  { path: '/warehouses', element: protectedPage(<WarehousePanel />) },
  { path: '/regions', element: protectedPage(<RegionPanel />) },
  { path: '/cancel-reasons', element: protectedPage(<CancelReasonPanel />) },
  { path: '/customers', element: protectedPage(<CustomerPanel />) },
  { path: '/customer-types', element: protectedPage(<CustomerTypePanel />) },
  { path: '/suppliers', element: protectedPage(<SupplierPanel />) },
  { path: '/products', element: protectedPage(<ProductPanel />) },
  { path: '/product-attributes', element: protectedPage(<ProductAttributePanel />) },
  { path: '/product-categories', element: protectedPage(<ProductCategoryPanel />) },
  { path: '/product-classes', element: protectedPage(<ProductClassPanel />) },
  { path: '/generics', element: protectedPage(<GenericPanel />) },
  { path: '/units-of-measure', element: protectedPage(<UnitOfMeasurePanel />) },
  { path: '/login', element: <LoginPage /> },
  { path: '/activate', element: <ActivationPage /> },
];

export default AppRoutes;
