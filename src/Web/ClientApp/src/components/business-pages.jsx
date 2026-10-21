import { useState } from 'react';
import {
  AccessClient,
  BranchClient,
  CancelReasonClient,
  CustomerClient,
  CustomerTypeClient,
  GenericClient,
  ProductAttributeClient,
  ProductCategoryClient,
  ProductClassClient,
  ProductClient,
  PsgcClient,
  RoleClient,
  SupplierClient,
  UnitOfMeasureClient,
  UsersClient,
  WarehouseClient,
} from '../web-api-client.ts';
import { createApiClient } from './api-client.js';
import { EntityPanel } from './common/EntityPanel.jsx';
import { ProductCommercialPanel } from './ProductCommercialPanel.jsx';

const clients = new Map();
function api(ClientType) {
  if (!clients.has(ClientType)) clients.set(ClientType, createApiClient(ClientType));
  return clients.get(ClientType);
}

const paged = (ClientType, method) => (page, pageSize, search) =>
  api(ClientType)[method](page, pageSize, search, undefined, undefined);
const all = (ClientType, method) => () => api(ClientType)[method]();
const field = (name, label, type = 'text', required = false, options, dependsOn) => ({
  name, label, type, required,
  ...(options ? { options } : {}),
  ...(dependsOn ? { dependsOn } : {}),
});
const gender = [
  { value: 0, label: 'Male' },
  { value: 1, label: 'Female' },
  { value: 2, label: 'Other' },
];
const civilStatus = [
  { value: 0, label: 'Single' },
  { value: 1, label: 'Married' },
  { value: 2, label: 'Separated' },
  { value: 3, label: 'Widower' },
  { value: 4, label: 'Other' },
];

const personFields = [
  field('supplierId', 'Supplier code'),
  field('firstName', 'First name', 'text', true),
  field('lastName', 'Last name', 'text', true),
  field('birthDate', 'Date of birth', 'date', true),
  field('gender', 'Gender', 'select-number', true, gender),
  field('civilStatus', 'Civil status', 'select-number', true, civilStatus),
  field('branchId', 'Branch ID', 'number', true),
];
const contactFields = [
  field('title', 'Title'),
  field('middleName', 'Middle name'),
  field('nameExt', 'Name extension'),
  field('email', 'Email', 'email'),
  field('billingAddress1', 'Billing address 1'),
  field('billingAddress2', 'Billing address 2'),
  field('phone', 'Phone'),
  field('mobile', 'Mobile'),
  field('fax', 'Fax'),
  field('tin', 'TIN'),
  field('contactPerson', 'Contact person'),
];
const personColumns = [
  { name: 'fullName', label: 'Name' },
  { name: 'email', label: 'Email' },
  { name: 'phone', label: 'Phone' },
  { name: 'created', label: 'Created', type: 'date' },
];

export const businessPageDefinitions = {
  access: {
    singular: 'Access claim',
    plural: 'Access claims',
    description: 'Manage features users and roles can access.',
    list: paged(AccessClient, 'getAccessClaims'),
    get: id => api(AccessClient).getAccessClaim(id),
    create: body => api(AccessClient).createAccessClaim(body),
    update: (id, body) => api(AccessClient).updateAccessClaim(id, body),
    remove: id => api(AccessClient).deleteAccessClaim(id),
    fields: [
      field('name', 'Name', 'text', true), 
      field('feature', 'Feature', 'text', true)],
    rowLink: item=> `/access/${encodeURIComponent(item.id)}/permission`,
    rowLinkLabel: "manage Permissions",
    columns: [
      { name: 'name', label: 'Name' }, 
      { name: 'feature', label: 'Feature' }
    ],
  },
  branches: {
    singular: 'Branch',
    plural: 'Branches',
    description: 'Manage branch locations and their contact information.',
    list: paged(BranchClient, 'getBranches'),
    get: id => api(BranchClient).getBranch(id),
    create: body => api(BranchClient).createBranch(body),
    update: (id, body) => api(BranchClient).updateBranch(id, body),
    remove: id => api(BranchClient).deleteBranch(id),
    fields: [
      field('name', 'Name', 'text', true),
      field('warehouseId', 'Warehouse', 'select', true, async () => (
        await api(WarehouseClient).getWarehouseList()
      ).map(warehouse => ({value: warehouse.id, label: warehouse.name }))),
      field('memo', 'Notes'),
      field('address1', 'Address 1'),
      field('address2', 'Address 2'),
      field('regionCode', 'Region', 'select', true, async () => (
        await api(PsgcClient).getRegions()
      ).map(region => ({ value: region.code, label: region.areaName }))),
      field('provinceCode', 'Province', 'select', true, async values => (
        await api(PsgcClient).getProvinces(values.regionCode)
      ).map(province => ({ value: province.code, label: province.areaName })), ['regionCode']),
      field('municipalityCode', 'City/Municipality', 'select', true, async values => (
        await api(PsgcClient).getMunicipalities(values.provinceCode)
      ).map(municipality => ({ value: municipality.code, label: municipality.areaName })), ['provinceCode']),
      field('barangayCode', 'Barangay', 'select', true, async values => (
        await api(PsgcClient).getBarangays(values.municipalityCode)
      ).map(barangay => ({ value: barangay.code, label: barangay.areaName })), ['municipalityCode']),
      field('phone', 'Phone'),
      field('fax', 'Fax'),
      field('email', 'Email', 'email', true),
      field('tin', 'TIN'),
    ],
    updateIncludesId: true,
    columns: [
      { name: 'name', label: 'Name' },
      { name: 'warehouse', label: 'Warehouse' },
      { name: 'region', label: 'Region' },
      { name: 'phone', label: 'Phone' },
    ],
  },
  'cancel-reasons': {
    singular: 'Cancel reason',
    plural: 'Cancel reasons',
    description: 'Configure cancellation reasons and memo requirements.',
    list: paged(CancelReasonClient, 'getCancelReasons'),
    get: id => api(CancelReasonClient).getCancelReason(id),
    create: body => api(CancelReasonClient).createCancelReason(body),
    update: (id, body) => api(CancelReasonClient).updateCancelReason(id, body),
    remove: id => api(CancelReasonClient).deleteCancelReason(id),
    fields: [
      field('name', 'Name', 'text', true),
      field('isActive', 'Active', 'checkbox'),
      field('isAllowUserToEnterMemo', 'Allow user memo', 'checkbox'),
      field('memo', 'Memo'),
    ],
    editFields: [field('memo', 'Memo')],
    updateFields: [field('memo', 'Memo')],
    updateIncludesId: true,
    columns: [
      { name: 'name', label: 'Name' },
      { name: 'isActive', label: 'Active', type: 'checkbox' },
      { name: 'isAllowUserToEnterMemo', label: 'Memo allowed', type: 'checkbox' },
      { name: 'memo', label: 'Memo' },
    ],
  },
  customers: {
    singular: 'Customer',
    plural: 'Customers',
    description: 'Maintain customer records and contact details.',
    list: paged(CustomerClient, 'getCustomers'),
    get: id => api(CustomerClient).getCustomer(id),
    create: body => api(CustomerClient).createCustomer(body),
    update: (id, body) => api(CustomerClient).updateCustomer(id, body),
    remove: id => api(CustomerClient).deleteCustomer(id),
    fields: [
      field('customerId', 'Customer code'),
      field('firstName', 'First name', 'text', true),
      field('lastName', 'Last name', 'text', true),
      field('birthDate', 'Date of birth', 'date', true),
      field('gender', 'Gender', 'select-number', true, gender),
      field('civilStatus', 'Civil status', 'select-number', true, civilStatus),
      field('branchId', 'Branch ID', 'number', true),
      field('customerTypeId', 'Customer type ID', 'number', true),
    ],
    editFields: [
      field('customerId', 'Customer code'),
      field('firstName', 'First name', 'text', true),
      field('lastName', 'Last name', 'text', true),
      field('birthDate', 'Date of birth', 'date', true),
      field('gender', 'Gender', 'select-number', true, gender),
      field('civilStatus', 'Civil status', 'select-number', true, civilStatus),
      field('branchId', 'Branch ID', 'number', true),
      field('customerTypeId', 'Customer type ID', 'number', true),
      ...contactFields,
    ],
    updateIncludesId: true,
    columns: personColumns,
  },
  'customer-types': {
    singular: 'Customer type',
    plural: 'Customer types',
    description: 'Maintain customer classification options.',
    list: paged(CustomerTypeClient, 'getCustomerTypes'),
    get: id => api(CustomerTypeClient).getCustomerType(id),
    create: body => api(CustomerTypeClient).createCustomerType(body),
    update: (id, body) => api(CustomerTypeClient).updateCustomerType(id, body),
    remove: id => api(CustomerTypeClient).deleteCustomerType(id),
    fields: [field('name', 'Name', 'text', true)],
    updateIncludesId: true,
    columns: [{ name: 'name', label: 'Name' }, { name: 'created', label: 'Created', type: 'date' }],
  },
  generics: {
    singular: 'Generic',
    plural: 'Generics',
    description: 'Manage generic medicine names.',
    list: paged(GenericClient, 'getGenericItems'),
    get: id => api(GenericClient).getGeneric(id),
    create: body => api(GenericClient).createGeneric(body),
    update: (id, body) => api(GenericClient).updateGeneric(id, body),
    remove: id => api(GenericClient).deleteGeneric(id),
    fields: [field('code', 'Code'), field('name', 'Name', 'text', true), field('memo', 'Notes')],
    updateIncludesId: true,
    columns: [{ name: 'code', label: 'Code' }, { name: 'name', label: 'Name' }, { name: 'memo', label: 'Notes' }],
  },
  products: {
    singular: 'Product',
    plural: 'Products',
    description: 'Maintain product catalog records.',
    list: paged(ProductClient, 'getProducts'),
    get: id => api(ProductClient).getProduct(id),
    create: body => api(ProductClient).createProduct(body),
    update: (id, body) => api(ProductClient).updateProduct(id, body),
    remove: id => api(ProductClient).deleteProduct(id),
    fields: [
      field('name', 'Name', 'text', true),
      field('productClassId', 'Product class ID', 'number', true),
      field('productTypeId', 'Product type ID', 'number', true),
      field('unitOfMeasureId', 'Unit of measure ID', 'number', true),
      field('memo', 'Notes'),
    ],
    editFields: [
      field('productClassId', 'Product class ID', 'number', true),
      field('productTypeId', 'Product type ID', 'number', true),
      field('unitOfMeasureId', 'Unit of measure ID', 'number', true),
      field('memo', 'Notes'),
    ],
    updateIncludesId: true,
    columns: [
      { name: 'name', label: 'Name' },
      { name: 'productClassId', label: 'Product class' },
      { name: 'unitOfMeasureId', label: 'Unit of measure' },
      { name: 'isActive', label: 'Active', type: 'checkbox' },
    ],
  },
  'product-attributes': {
    singular: 'Product attribute',
    plural: 'Product attributes',
    description: 'Manage descriptive attributes available to products.',
    list: paged(ProductAttributeClient, 'getProductAttributes'),
    get: id => api(ProductAttributeClient).getProductAttribute(id),
    create: body => api(ProductAttributeClient).createProductAttribute(body),
    update: (id, body) => api(ProductAttributeClient).updateProductAttribute(id, body),
    remove: id => api(ProductAttributeClient).deleteProductAttribute(id),
    fields: [field('name', 'Name', 'text', true)],
    updateIncludesId: true,
    columns: [{ name: 'name', label: 'Name' }, { name: 'created', label: 'Created', type: 'date' }],
  },
  'product-categories': {
    singular: 'Product category',
    plural: 'Product categories',
    description: 'Maintain product categories.',
    list: all(ProductCategoryClient, 'getProductCategories'),
    paged: false,
    create: body => api(ProductCategoryClient).createProductCategory(body),
    remove: id => api(ProductCategoryClient).deleteProductCategory(id),
    fields: [field('name', 'Name', 'text', true)],
    columns: [{ name: 'name', label: 'Name' }, { name: 'created', label: 'Created', type: 'date' }],
  },
  'product-classes': {
    singular: 'Product class',
    plural: 'Product classes',
    description: 'Set product class, tax, unit, and generic associations.',
    list: paged(ProductClassClient, 'getProductClasses'),
    get: id => api(ProductClassClient).getProductClass(id),
    create: body => api(ProductClassClient).createProductClass(body),
    update: (id, body) => api(ProductClassClient).updateProductClass(id, body),
    remove: id => api(ProductClassClient).deleteProductClass(id),
    fields: [
      field('name', 'Name', 'text', true),
      field('memo', 'Notes'),
      field('productTypeId', 'Product type ID', 'number', true),
      field('taxTypeId', 'Tax type ID', 'number', true),
      field('unitOfMeasurementId', 'Unit of measure ID', 'number', true),
      field('genericNameId', 'Generic name ID', 'number', true),
    ],
    editFields: [
      field('memo', 'Notes'),
      field('productTypeId', 'Product type ID', 'number', true),
      field('taxTypeId', 'Tax type ID', 'number', true),
      field('unitOfMeasurementId', 'Unit of measure ID', 'number', true),
      field('genericNameId', 'Generic name ID', 'number', true),
    ],
    updateIncludesId: true,
    columns: [{ name: 'name', label: 'Name' }, { name: 'productTypeId', label: 'Product type' }, { name: 'memo', label: 'Notes' }],
  },
  roles: {
    singular: 'Role',
    plural: 'Roles',
    description: 'Manage user roles. Access claims can be assigned from the role access screen.',
    list: paged(RoleClient, 'getRoles'),
    get: id => api(RoleClient).getRole(id),
    create: body => api(RoleClient).createRole(body),
    remove: id => api(RoleClient).deleteRole(id),
    fields: [field('name', 'Name', 'text', true)],
    rowLink: item => `/roles/${encodeURIComponent(item.id)}/access`,
    rowLinkLabel: 'Manage Access',
    columns: [{ name: 'name', label: 'Name' }],
  },
  suppliers: {
    singular: 'Supplier',
    plural: 'Suppliers',
    description: 'Maintain supplier records and contact details.',
    list: paged(SupplierClient, 'getSuppliers'),
    get: id => api(SupplierClient).getSupplier(id),
    create: body => api(SupplierClient).createSupplier(body),
    update: (id, body) => api(SupplierClient).updateSupplier(id, body),
    remove: id => api(SupplierClient).deleteSupplier(id),
    fields: personFields,
    editFields: [...personFields, ...contactFields],
    updateIncludesId: true,
    columns: personColumns,
  },
  'units-of-measure': {
    singular: 'Unit of measure',
    plural: 'Units of measure',
    description: 'Define product quantity units and conversion behavior.',
    list: paged(UnitOfMeasureClient, 'getUnitOfMeasures'),
    get: id => api(UnitOfMeasureClient).getUnitOfMeasure(id),
    create: body => api(UnitOfMeasureClient).createUnitOfMeasure(body),
    update: (id, body) => api(UnitOfMeasureClient).updateUnitOfMeasure(id, body),
    remove: id => api(UnitOfMeasureClient).deleteUnitOfMeasure(id),
    fields: [
      field('unitOfMeasureId', 'Unit code', 'text', true),
      field('name', 'Name', 'text', true),
      field('unitBase', 'Base unit', 'text', true),
      field('isGroupAsSingleQuantity', 'Group as one quantity', 'checkbox'),
      field('isAsForQtyWhenSold', 'Ask quantity when sold', 'checkbox'),
      field('decimalPlaces', 'Decimal places', 'number', true),
    ],
    editFields: [
      field('name', 'Name', 'text', true),
      field('unitBase', 'Base unit', 'text', true),
      field('isGroupAsSingleQuantity', 'Group as one quantity', 'checkbox'),
      field('isAsForQtyWhenSold', 'Ask quantity when sold', 'checkbox'),
      field('decimalPlaces', 'Decimal places', 'number', true),
    ],
    updateIncludesId: true,
    columns: [
      { name: 'id', label: 'ID' },
      { name: 'name', label: 'Name' },
      { name: 'unitBase', label: 'Base unit' },
      { name: 'decimalPlaces', label: 'Decimal places' },
    ],
  },
  users: {
    singular: 'User',
    plural: 'Users',
    description: 'Manage employee accounts and their branch assignments.',
    list: paged(UsersClient, 'getUsers'),
    get: id => api(UsersClient).getUser(id),
    create: body => api(UsersClient).createUser(body),
    update: (id, body) => api(UsersClient).updateUser(id, body),
    remove: id => api(UsersClient).deleteUser(id),
    fields: [
      field('employeeId', 'Employee ID', 'text', true),
      field('firstName', 'First name', 'text', true),
      field('lastName', 'Last name', 'text', true),
      field('birthDate', 'Date of birth', 'date', true),
      field('gender', 'Gender', 'select-number', true, gender),
      field('civilStatus', 'Civil status', 'select-number', true, civilStatus),
      field('branchId', 'Branch', 'select', true, async () => (
        await api(BranchClient).getBranchesList()
      ).map(branch => ({value: branch.id, label: branch.name }))),
      field('email', 'Email', 'email', true),
      field('roleId', 'Role', 'select', true, async () => (
        await api(RoleClient).getRolesList()
      ).map(role => ({ value: role.id, label: role.name }))),
    ],
    editFields: [
      field('employeeId', 'Employee ID', 'text', true),
      field('firstName', 'First name', 'text', true),
      field('lastName', 'Last name', 'text', true),
      field('birthDate', 'Date of birth', 'date', true),
      field('gender', 'Gender', 'select-number', true, gender),
      field('civilStatus', 'Civil status', 'select-number', true, civilStatus),
    ],
    updateIncludesId: true,
    columns: personColumns,
  },
  warehouses: {
    singular: 'Warehouse',
    plural: 'Warehouses',
    description: 'Manage warehouse locations and contact details.',
    list: paged(WarehouseClient, 'getWarehouses'),
    get: id => api(WarehouseClient).getWarehouse(id),
    create: body => api(WarehouseClient).createWarehouse(body),
    update: (id, body) => api(WarehouseClient).updateWarehouse(id, body),
    remove: id => api(WarehouseClient).deleteWarehouse(id),
    fields: [
      field('warehouseId', 'Warehouse code', 'text', true),
      field('name', 'Name', 'text', true),
      field('memo', 'Notes'),
      field('address1', 'Address 1'),
      field('address2', 'Address 2'),
      field('regionCode', 'Region', 'select', true, async () => (
        await api(PsgcClient).getRegions()
      ).map(region => ({ value: region.code, label: region.areaName }))),
      field('provinceCode', 'Province', 'select', true, async values => (
        await api(PsgcClient).getProvinces(values.regionCode)
      ).map(province => ({ value: province.code, label: province.areaName })), ['regionCode']),
      field('municipalityCode', 'City/Municipality', 'select', true, async values => (
        await api(PsgcClient).getMunicipalities(values.provinceCode)
      ).map(municipality => ({ value: municipality.code, label: municipality.areaName })), ['provinceCode']),
      field('barangayCode', 'Barangay', 'select', true, async values => (
        await api(PsgcClient).getBarangays(values.municipalityCode)
      ).map(barangay => ({ value: barangay.code, label: barangay.areaName })), ['municipalityCode']),
      field('phone', 'Phone'),
      field('fax', 'Fax'),
      field('email', 'Email', 'email'),
    ],
    editFields: [
      field('name', 'Name', 'text', true),
      field('memo', 'Notes'),
      field('address1', 'Address 1'),
      field('address2', 'Address 2'),
      field('regionCode', 'Region', 'select', true, async () => (
        await api(PsgcClient).getRegions()
      ).map(region => ({ value: region.code, label: region.areaName }))),
      field('provinceCode', 'Province', 'select', true, async values => (
        await api(PsgcClient).getProvinces(values.regionCode)
      ).map(province => ({ value: province.code, label: province.areaName })), ['regionCode']),
      field('municipalityCode', 'City/Municipality', 'select', true, async values => (
        await api(PsgcClient).getMunicipalities(values.provinceCode)
      ).map(municipality => ({ value: municipality.code, label: municipality.areaName })), ['provinceCode']),
      field('barangayCode', 'Barangay', 'select', true, async values => (
        await api(PsgcClient).getBarangays(values.municipalityCode)
      ).map(barangay => ({ value: barangay.code, label: barangay.areaName })), ['municipalityCode']),
      field('phone', 'Phone'),
      field('fax', 'Fax'),
      field('email', 'Email', 'email'),
    ],
    updateIncludesId: true,
    columns: [
      { name: 'warehouseId', label: 'Code' },
      { name: 'name', label: 'Name' },
      { name: 'email', label: 'Email Address' },
      { name: 'phone', label: 'Phone' },
    ],
  },
};

export function BusinessPage({ resource }) {
  const definition = businessPageDefinitions[resource];
  if (!definition) return <p role="alert">Unknown business page.</p>;
  return <EntityPanel definition={definition} />;
}

const makePanel = resource => function BusinessPanel() {
  return <BusinessPage resource={resource} />;
};

export const AccessPanel = makePanel('access');
export const BranchPanel = makePanel('branches');
export const CancelReasonPanel = makePanel('cancel-reasons');
export const CustomerPanel = makePanel('customers');
export const CustomerTypePanel = makePanel('customer-types');
export const GenericPanel = makePanel('generics');
export function ProductPanel() {
  return (
    <>
      <BusinessPage resource="products" />
      <ProductCommercialPanel />
    </>
  );
}
export const ProductAttributePanel = makePanel('product-attributes');
export const ProductCategoryPanel = makePanel('product-categories');
export const ProductClassPanel = makePanel('product-classes');
export const RegionPanel = makePanel('regions');
export const RolePanel = makePanel('roles');
export const SupplierPanel = makePanel('suppliers');
export function UsersPanel() {
  return (
    <>
      <BusinessPage resource="users" />
      <UserAssignmentsPanel />
    </>
  );
}
export const WarehousePanel = makePanel('warehouses');
export const UnitOfMeasurePanel = makePanel('units-of-measure');

function UserAssignmentsPanel() {
  const [userId, setUserId] = useState('');
  const [accesses, setAccesses] = useState([]);
  const [roles, setRoles] = useState([]);
  const [availableAccesses, setAvailableAccesses] = useState([]);
  const [selectedAccessId, setSelectedAccessId] = useState('');
  const [newRoleId, setNewRoleId] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [busy, setBusy] = useState(false);

  async function loadAssignments(id) {
    if (!id) return;
    setLoading(true);
    setError('');
    try {
      const numericId = Number(id);
      const [userAccesses, userRoles, accessList] = await Promise.all([
        api(UsersClient).getUserAccess(numericId),
        api(UsersClient).getUserRoles(numericId),
        api(AccessClient).getAccessClaimsList(),
      ]);
      setAccesses(userAccesses ?? []);
      setRoles(userRoles ?? []);
      setAvailableAccesses(accessList ?? []);
    } catch (requestError) {
      setError(requestError?.message || "Could not load this user's access assignments.");
    } finally {
      setLoading(false);
    }
  }

  async function submitUser(event) {
    event.preventDefault();
    await loadAssignments(userId);
  }

  async function addAccess(event) {
    event.preventDefault();
    setBusy(true);
    try {
      await api(UsersClient).addUserAccess(Number(userId), Number(selectedAccessId));
      setSelectedAccessId('');
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not assign the access claim.');
    } finally {
      setBusy(false);
    }
  }

  async function changePermission(access, permission, value) {
    setBusy(true);
    try {
      await api(UsersClient).updateUserAccess(Number(userId), access.accessId, {
        userId: Number(userId),
        accessId: access.accessId,
        hasRead: permission === 'hasRead' ? value : Boolean(access.hasRead),
        hasWrite: permission === 'hasWrite' ? value : Boolean(access.hasWrite),
        hasUpdate: permission === 'hasUpdate' ? value : Boolean(access.hasUpdate),
        hasDelete: permission === 'hasDelete' ? value : Boolean(access.hasDelete),
        isActive: permission === 'isActive' ? value : Boolean(access.isActive),
      });
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not update the access claim.');
    } finally {
      setBusy(false);
    }
  }

  async function removeAccess(accessId) {
    setBusy(true);
    try {
      await api(UsersClient).removeUserAccess(Number(userId), accessId);
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not remove the access claim.');
    } finally {
      setBusy(false);
    }
  }

  async function assignRole(event) {
    event.preventDefault();
    setBusy(true);
    try {
      await api(UsersClient).assignUserRole(Number(userId), newRoleId);
      setNewRoleId('');
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not assign the role.');
    } finally {
      setBusy(false);
    }
  }

  async function removeRole(roleId) {
    setBusy(true);
    try {
      await api(UsersClient).dismissUserRole(Number(userId), roleId);
      await loadAssignments(userId);
    } catch (requestError) {
      setError(requestError?.message || 'Could not remove the role.');
    } finally {
      setBusy(false);
    }
  }

  return (
    <section className="admin-section user-assignments">
      <header className="entity-toolbar">
        <div>
          <h2>User access and roles</h2>
          <p>Manage access claims and roles assigned to an employee account.</p>
        </div>
      </header>
      <form className="entity-search" onSubmit={submitUser}>
        <label htmlFor="assignment-user-id">User ID</label>
        <input id="assignment-user-id" type="number" min="1" value={userId} required
          onChange={event => setUserId(event.target.value)} />
        <button type="submit" disabled={loading}>Load assignments</button>
      </form>
      {error && <p className="error" role="alert">{error}</p>}
      {loading && <p aria-busy="true">Loading assignments...</p>}
      {userId && !loading && (
        <>
          <form className="entity-search" onSubmit={addAccess}>
            <select aria-label="Access claim" value={selectedAccessId} required
              onChange={event => setSelectedAccessId(event.target.value)}>
              <option value="">Choose an access claim</option>
              {availableAccesses
                .filter(item => !accesses.some(access => access.accessId === item.id))
                .map(item => <option key={item.id} value={item.id}>{item.name}</option>)}
            </select>
            <button type="submit" disabled={busy}>Assign access</button>
          </form>
          <div className="admin-table-wrap">
            <table className="admin-table">
              <thead><tr><th>Access claim</th><th>Read</th><th>Write</th><th>Update</th><th>Delete</th><th>Active</th><th /></tr></thead>
              <tbody>
                {accesses.map(access => (
                  <tr key={access.accessId}>
                    <th scope="row">{access.name}</th>
                    {['hasRead', 'hasWrite', 'hasUpdate', 'hasDelete', 'isActive'].map(permission => (
                      <td key={permission}>
                        <input
                          type="checkbox"
                          aria-label={`${permission} for ${access.name}`}
                          checked={Boolean(access[permission])}
                          disabled={busy}
                          onChange={event => changePermission(access, permission, event.target.checked)}
                        />
                      </td>
                    ))}
                    <td><button type="button" className="contrast" disabled={busy}
                      onClick={() => removeAccess(access.accessId)}>Remove</button></td>
                  </tr>
                ))}
                {!accesses.length && <tr><td colSpan="7">No access claims assigned.</td></tr>}
              </tbody>
            </table>
          </div>
          <form className="entity-search" onSubmit={assignRole}>
            <input aria-label="Role ID" placeholder="Role ID" value={newRoleId} required
              onChange={event => setNewRoleId(event.target.value)} />
            <button type="submit" disabled={busy}>Assign role</button>
          </form>
          <ul className="assignment-list">
            {roles.map(role => (
              <li key={role.roleId}>
                <span>{role.roleName || role.roleId}</span>
                <button type="button" className="contrast" disabled={busy}
                  onClick={() => removeRole(role.roleId)}>Remove role</button>
              </li>
            ))}
            {!roles.length && <li>No roles assigned.</li>}
          </ul>
        </>
      )}
    </section>
  );
}
