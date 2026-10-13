import { useState } from 'react';
import { ProductClient } from '../web-api-client.ts';
import { createApiClient } from './api-client.js';

const products = createApiClient(ProductClient);
const emptySale = {
  shortName: '',
  isSellThisItem: true,
  isSellIteminWeb: false,
  taxType: '',
  markUp: '',
  standardCost: '',
  lastPrice: '',
  seniorTax: false,
  pwdTax: false,
  isSubjectToAmusement: false,
  isSubjectToSoloParentDiscount: false,
};
const emptyPurchasing = {
  purchasingUnitId: '',
  primarySupplierId: '',
  maxQuantity: '',
  reorderQuantity: '',
};

function numberOrUndefined(value) {
  return value === '' ? undefined : Number(value);
}

export function ProductCommercialPanel() {
  const [productId, setProductId] = useState('');
  const [sale, setSale] = useState(null);
  const [purchasing, setPurchasing] = useState(null);
  const [saleForm, setSaleForm] = useState(emptySale);
  const [purchasingForm, setPurchasingForm] = useState(emptyPurchasing);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [busy, setBusy] = useState(false);

  async function loadDetails(id = Number(productId)) {
    setLoading(true);
    setError('');
    setSale(null);
    setPurchasing(null);
    setSaleForm(emptySale);
    setPurchasingForm(emptyPurchasing);

    const readOrMissing = async request => {
      try {
        return await request();
      } catch (requestError) {
        if (requestError?.status === 404) return null;
        throw requestError;
      }
    };

    try {
      const [saleResult, purchasingResult] = await Promise.all([
        readOrMissing(() => products.getProductSale(id)),
        readOrMissing(() => products.getProductPurchasing(id)),
      ]);
      setSale(saleResult);
      setPurchasing(purchasingResult);
      if (saleResult) {
        setSaleForm(Object.fromEntries(
          Object.keys(emptySale).map(key => [key, saleResult[key] ?? emptySale[key]]),
        ));
      }
      if (purchasingResult) {
        setPurchasingForm(Object.fromEntries(
          Object.keys(emptyPurchasing).map(key => [key, purchasingResult[key] ?? emptyPurchasing[key]]),
        ));
      }
    } catch (requestError) {
      setError(requestError?.message || 'Could not load product sale and purchasing details.');
    } finally {
      setLoading(false);
    }
  }

  async function submitLoad(event) {
    event.preventDefault();
    await loadDetails();
  }

  async function createSale(event) {
    event.preventDefault();
    setBusy(true);
    setError('');
    try {
      await products.createProductSale({
        productId: Number(productId),
        ...saleForm,
        taxType: numberOrUndefined(saleForm.taxType),
        markUp: numberOrUndefined(saleForm.markUp),
        standardCost: numberOrUndefined(saleForm.standardCost),
        lastPrice: numberOrUndefined(saleForm.lastPrice),
      });
      await loadDetails();
    } catch (requestError) {
      setError(requestError?.message || 'Could not create product sale details.');
    } finally {
      setBusy(false);
    }
  }

  async function createPurchasing(event) {
    event.preventDefault();
    setBusy(true);
    setError('');
    try {
      await products.createProductPurchasing({
        productId: Number(productId),
        purchasingUnitId: Number(purchasingForm.purchasingUnitId),
        primarySupplierId: Number(purchasingForm.primarySupplierId),
        maxQuantity: numberOrUndefined(purchasingForm.maxQuantity),
        reorderQuantity: numberOrUndefined(purchasingForm.reorderQuantity),
      });
      await loadDetails();
    } catch (requestError) {
      setError(requestError?.message || 'Could not create product purchasing details.');
    } finally {
      setBusy(false);
    }
  }

  function updateForm(setter, name, value) {
    setter(current => ({ ...current, [name]: value }));
  }

  function checkboxField(label, name, value, setter, disabled = false) {
    return (
      <label className="commercial-checkbox" key={name}>
        <input type="checkbox" checked={Boolean(value)} disabled={disabled}
          onChange={event => updateForm(setter, name, event.target.checked)} />
        {label}
      </label>
    );
  }

  return (
    <section className="admin-section product-commercial">
      <header className="entity-toolbar">
        <div>
          <h2>Product sale and purchasing setup</h2>
          <p>View or create the sale and supplier settings exposed by the product API.</p>
        </div>
      </header>
      <form className="entity-search" onSubmit={submitLoad}>
        <label htmlFor="commercial-product-id">Product ID</label>
        <input id="commercial-product-id" type="number" min="1" value={productId} required
          onChange={event => setProductId(event.target.value)} />
        <button type="submit" disabled={loading}>{loading ? 'Loading...' : 'Load setup'}</button>
      </form>
      {error && <p role="alert" className="error">{error}</p>}
      {productId && (
        <div className="commercial-grid">
          <section className="commercial-card">
            <h3>Sale</h3>
            {sale ? (
              <dl className="commercial-details">
                <dt>Short name</dt><dd>{sale.shortName || '—'}</dd>
                <dt>Can sell</dt><dd>{sale.isSellThisItem ? 'Yes' : 'No'}</dd>
                <dt>Web sales</dt><dd>{sale.isSellIteminWeb ? 'Yes' : 'No'}</dd>
                <dt>Standard cost</dt><dd>{sale.standardCost ?? '—'}</dd>
                <dt>Last price</dt><dd>{sale.lastPrice ?? '—'}</dd>
              </dl>
            ) : (
              <form onSubmit={createSale}>
                <label>Short name
                  <input value={saleForm.shortName} onChange={event => updateForm(setSaleForm, 'shortName', event.target.value)} />
                </label>
                <label>Tax type
                  <input type="number" value={saleForm.taxType} onChange={event => updateForm(setSaleForm, 'taxType', event.target.value)} />
                </label>
                <label>Markup
                  <input type="number" step="any" value={saleForm.markUp} onChange={event => updateForm(setSaleForm, 'markUp', event.target.value)} />
                </label>
                <label>Standard cost
                  <input type="number" step="any" value={saleForm.standardCost} onChange={event => updateForm(setSaleForm, 'standardCost', event.target.value)} />
                </label>
                <label>Last price
                  <input type="number" step="any" value={saleForm.lastPrice} onChange={event => updateForm(setSaleForm, 'lastPrice', event.target.value)} />
                </label>
                {checkboxField('Available for sale', 'isSellThisItem', saleForm.isSellThisItem, setSaleForm)}
                {checkboxField('Sell online', 'isSellIteminWeb', saleForm.isSellIteminWeb, setSaleForm)}
                {checkboxField('Senior discount', 'seniorTax', saleForm.seniorTax, setSaleForm)}
                {checkboxField('PWD discount', 'pwdTax', saleForm.pwdTax, setSaleForm)}
                {checkboxField('Amusement tax', 'isSubjectToAmusement', saleForm.isSubjectToAmusement, setSaleForm)}
                {checkboxField('Solo-parent discount', 'isSubjectToSoloParentDiscount', saleForm.isSubjectToSoloParentDiscount, setSaleForm)}
                <button type="submit" disabled={busy || loading}>Create sale setup</button>
              </form>
            )}
          </section>
          <section className="commercial-card">
            <h3>Purchasing</h3>
            {purchasing ? (
              <dl className="commercial-details">
                <dt>Purchasing unit ID</dt><dd>{purchasing.purchasingUnitId ?? '—'}</dd>
                <dt>Primary supplier ID</dt><dd>{purchasing.primarySupplierId ?? '—'}</dd>
                <dt>Maximum quantity</dt><dd>{purchasing.maxQuantity ?? '—'}</dd>
                <dt>Reorder quantity</dt><dd>{purchasing.reorderQuantity ?? '—'}</dd>
              </dl>
            ) : (
              <form onSubmit={createPurchasing}>
                <label>Purchasing unit ID
                  <input type="number" min="1" value={purchasingForm.purchasingUnitId} required
                    onChange={event => updateForm(setPurchasingForm, 'purchasingUnitId', event.target.value)} />
                </label>
                <label>Primary supplier ID
                  <input type="number" min="1" value={purchasingForm.primarySupplierId} required
                    onChange={event => updateForm(setPurchasingForm, 'primarySupplierId', event.target.value)} />
                </label>
                <label>Maximum quantity
                  <input type="number" min="0" step="any" value={purchasingForm.maxQuantity}
                    onChange={event => updateForm(setPurchasingForm, 'maxQuantity', event.target.value)} />
                </label>
                <label>Reorder quantity
                  <input type="number" min="0" step="any" value={purchasingForm.reorderQuantity}
                    onChange={event => updateForm(setPurchasingForm, 'reorderQuantity', event.target.value)} />
                </label>
                <button type="submit" disabled={busy || loading}>Create purchasing setup</button>
              </form>
            )}
          </section>
        </div>
      )}
      {productId && (sale || purchasing) && (
        <p className="helper-text">The generated API exposes read and create operations for these settings; it does not expose update or delete operations.</p>
      )}
    </section>
  );
}
