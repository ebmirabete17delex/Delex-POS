import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { Link } from 'react-router-dom';
import {
  ChevronLeft,
  ChevronRight,
  LoaderCircle,
  Pencil,
  Plus,
  RefreshCw,
  Save,
  Search,
  Trash2,
  X,
  ShieldCogCorner
} from 'lucide-react';

const PAGE_SIZE = 7;

function ModalDialog({ children, className = '', onClose, labelledBy }) {
  const dialogRef = useRef(null);

  useEffect(() => {
    const dialog = dialogRef.current;
    if (!dialog) return undefined;
    dialog.showModal();
    return () => {
      if (dialog.open) dialog.close();
    };
  }, []);

  return (
    <dialog
      ref={dialogRef}
      className={`entity-dialog ${className}`}
      aria-labelledby={labelledBy}
      onCancel={event => {
        event.preventDefault();
        onClose();
      }}
      onClick={event => {
        if (event.target === event.currentTarget) onClose();
      }}
    >
      {children}
    </dialog>
  );
}

function toInputValue(field, value) {
  if (value == null) return field.type === 'checkbox' ? false : '';
  if (field.type === 'date') {
    const date = new Date(value);
    return Number.isNaN(date.getTime()) ? '' : date.toISOString().slice(0, 10);
  }
  return value;
}

function toPayload(fields, value) {
  const payload = {};
  for (const field of fields) {
    const raw = value[field.name];
    if (raw === '' || raw === undefined) {
      if (field.required) payload[field.name] = raw;
      continue;
    }
    if (field.type === 'number' || field.type === 'select-number') {
      payload[field.name] = Number(raw);
    } else if (field.type === 'checkbox') {
      payload[field.name] = Boolean(raw);
    } else if (field.type === 'date') {
      payload[field.name] = new Date(raw);
    } else {
      payload[field.name] = raw;
    }
  }
  return payload;
}

function displayValue(value, field) {
  if (value == null || value === '') return '—';
  if (field?.type === 'checkbox') return value ? 'Yes' : 'No';
  if (field?.type === 'date') {
    const date = new Date(value);
    return Number.isNaN(date.getTime()) ? String(value) : date.toLocaleDateString();
  }
  if (field?.type === 'select-number' || field?.type === 'select') {
    const options = Array.isArray(field.options) ? field.options : [];
    return options.find(option => String(option.value) === String(value))?.label ?? value;
  }
  return String(value);
}

function getDependentFields(fields, fieldName) {
  const dependentNames = new Set();
  let changed = true;
  while (changed) {
    changed = false;
    for (const field of fields) {
      if (!dependentNames.has(field.name)
        && field.dependsOn?.some(dependency => dependency === fieldName || dependentNames.has(dependency))) {
        dependentNames.add(field.name);
        changed = true;
      }
    }
  }
  return fields.filter(field => dependentNames.has(field.name));
}

export function EntityPanel({ definition }) {
  const [items, setItems] = useState([]);
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [searchValue, setSearchValue] = useState('');
  const [editor, setEditor] = useState(null);
  const [deleteTarget, setDeleteTarget] = useState(null);
  const [loading, setLoading] = useState(true);
  const [busy, setBusy] = useState(false);
  const [deleteBusy, setDeleteBusy] = useState(false);
  const [error, setError] = useState('');
  const [formError, setFormError] = useState('');
  const [fieldOptions, setFieldOptions] = useState({});
  const [optionsLoading, setOptionsLoading] = useState({});
  const pageRef = useRef(page);
  const searchRef = useRef('');
  const requestRef = useRef(0);
  const optionRequestRef = useRef({});

  const fields = useMemo(
    () => editor?.mode === 'edit' ? definition.editFields ?? definition.fields : definition.fields,
    [definition, editor?.mode],
  );

  const refresh = useCallback(async (requestedPage = pageRef.current, requestedSearch = searchRef.current) => {
    const requestId = ++requestRef.current;
    setLoading(true);
    setError('');
    try {
      const response = await definition.list(requestedPage, PAGE_SIZE, requestedSearch || undefined);
      if (requestId !== requestRef.current) return;
      const responseItems = Array.isArray(response)
        ? response
        : response?.items ?? response?.lists ?? response?.data;
      if (!Array.isArray(responseItems)) {
        throw new Error('The API response did not contain a list.');
      }
      const source = definition.paged === false && requestedSearch
        ? responseItems.filter(item => definition.columns.some(column =>
          String(item[column.name] ?? '').toLowerCase().includes(requestedSearch.toLowerCase())))
        : responseItems;

      if (definition.paged === false || Array.isArray(response)) {
        const pages = Math.max(1, Math.ceil(source.length / PAGE_SIZE));
        const boundedPage = Math.min(Math.max(requestedPage, 1), pages);
        setItems(source.slice((boundedPage - 1) * PAGE_SIZE, boundedPage * PAGE_SIZE));
        setPage(boundedPage);
        pageRef.current = boundedPage;
        setTotalPages(pages);
        setTotalCount(source.length);
      } else {
        const pages = Math.max(1, response?.totalPages ?? 1);
        const boundedPage = Math.min(Math.max(requestedPage, 1), pages);
        if (boundedPage !== requestedPage) {
          const corrected = await definition.list(boundedPage, PAGE_SIZE, requestedSearch || undefined);
          if (requestId !== requestRef.current) return;
          const correctedItems = corrected?.items ?? corrected?.lists ?? corrected?.data;
          if (!Array.isArray(correctedItems)) throw new Error('The API response did not contain a list.');
          setItems(correctedItems);
          setPage(corrected?.pageNumber ?? boundedPage);
          pageRef.current = corrected?.pageNumber ?? boundedPage;
          setTotalPages(Math.max(1, corrected?.totalPages ?? boundedPage));
          setTotalCount(corrected?.totalCount ?? correctedItems.length);
          return;
        }
        setItems(source);
        setPage(response?.pageNumber ?? boundedPage);
        pageRef.current = response?.pageNumber ?? boundedPage;
        setTotalPages(pages);
        setTotalCount(response?.totalCount ?? source.length);
      }
    } catch (requestError) {
      if (requestId === requestRef.current) {
        setError(requestError?.message || `Could not load ${definition.plural.toLowerCase()}.`);
      }
    } finally {
      if (requestId === requestRef.current) setLoading(false);
    }
  }, [definition]);

  useEffect(() => {
    refresh(1, '');
    return () => { requestRef.current += 1; };
  }, [refresh]);

  async function loadFieldOptions(fieldsToLoad, values) {
    await Promise.all(fieldsToLoad.map(async field => {
      const requestId = (optionRequestRef.current[field.name] ?? 0) + 1;
      optionRequestRef.current[field.name] = requestId;
      const hasDependencies = field.dependsOn?.every(dependency => (
        values[dependency] !== undefined && values[dependency] !== null && values[dependency] !== ''
      )) ?? true;

      if (!hasDependencies) {
        setFieldOptions(current => ({ ...current, [field.name]: [] }));
        setOptionsLoading(current => ({ ...current, [field.name]: false }));
        return;
      }

      setOptionsLoading(current => ({
        ...current,
        [field.name]: typeof field.options === 'function',
      }));
      try {
        try {
          const options = typeof field.options === 'function'
            ? await field.options(values)
            : field.options;
          if (options != null && !Array.isArray(options)) {
            throw new Error(`Options for ${field.label} must be a list.`);
          }
          if (optionRequestRef.current[field.name] === requestId) {
            setFieldOptions(current => ({ ...current, [field.name]: options ?? [] }));
          }
        } catch (requestError) {
          if (optionRequestRef.current[field.name] === requestId) throw requestError;
        }
      } finally {
        if (optionRequestRef.current[field.name] === requestId) {
          setOptionsLoading(current => ({ ...current, [field.name]: false }));
        }
      }
    }));
  }

  function handleSelectChange(field, value) {
    const nextValues = { ...editor.value, [field.name]: value };
    const dependentFields = getDependentFields(fields, field.name);
    for (const dependentField of dependentFields) {
      nextValues[dependentField.name] = '';
    }
    setEditor(current => current && ({ ...current, value: nextValues }));
    setFieldOptions(current => {
      const nextOptions = { ...current };
      for (const dependentField of dependentFields) {
        nextOptions[dependentField.name] = [];
      }
      return nextOptions;
    });

    const directDependents = fields.filter(candidate => candidate.dependsOn?.includes(field.name));
    loadFieldOptions(directDependents, nextValues).catch(requestError => {
      setFormError(requestError?.message || `Could not load options for ${directDependents[0]?.label ?? field.label}.`);
    });
  }

  async function openEditor(item) {
    setFormError('');
    setError('');
    const editorFields = item ? definition.editFields ?? definition.fields : definition.fields;
    try {
      setBusy(true);
      let values = {};
      let details;
      if (item) {
        details = definition.get ? await definition.get(item[definition.idField ?? 'id']) : item;
        if (!details) throw new Error(`Could not load ${definition.singular.toLowerCase()} details.`);
        for (const field of editorFields) {
          values[field.name] = toInputValue(field, details[field.name]);
        }
      }
      await loadFieldOptions(editorFields, values);
      if (item) {
        const idField = definition.idField ?? 'id';
        setEditor({ mode: 'edit', value: values, item: { ...details, [idField]: item[idField] } });
      } else {
        setEditor({ mode: 'create', value: values });
      }
    } catch (requestError) {
      setError(requestError?.message || `Could not load ${definition.singular.toLowerCase()} details.`);
    } finally {
      setBusy(false);
    }
  }

  async function save(event) {
    event.preventDefault();
    setBusy(true);
    setFormError('');
    try {
      const update = editor.mode === 'edit';
      const fieldsToSend = update
        ? definition.updateFields ?? definition.editFields ?? definition.fields
        : definition.fields;
      const body = toPayload(fieldsToSend, editor.value);
      if (update && definition.updateIncludesId) {
        body.id = editor.item[definition.idField ?? 'id'];
      }

      if (update) {
        await definition.update(editor.item[definition.idField ?? 'id'], body);
      } else {
        await definition.create(body);
      }
      setEditor(null);
      await refresh(update ? pageRef.current : 1, searchRef.current);
    } catch (requestError) {
      setFormError(requestError?.message || `Could not save ${definition.singular.toLowerCase()}.`);
    } finally {
      setBusy(false);
    }
  }

  async function remove() {
    if (!deleteTarget) return;
    setDeleteBusy(true);
    setError('');
    try {
      await definition.remove(deleteTarget[definition.idField ?? 'id']);
      setDeleteTarget(null);
      await refresh(pageRef.current, searchRef.current);
    } catch (requestError) {
      setError(requestError?.message || `Could not delete ${definition.singular.toLowerCase()}.`);
    } finally {
      setDeleteBusy(false);
    }
  }

  function submitSearch(event) {
    event.preventDefault();
    searchRef.current = searchValue.trim();
    pageRef.current = 1;
    setPage(1);
    refresh(1, searchValue.trim());
  }

  return (
    <section className="admin-section entity-panel">
      <header className="entity-toolbar">
        <div>
          <h2>{definition.plural}</h2>
          <p>{definition.description}</p>
        </div>
        <div className="entity-toolbar-actions">
          <button type="button" className="secondary" onClick={() => refresh()} disabled={loading}>
            <RefreshCw size={16} aria-hidden="true" className={loading ? 'icon-spin' : undefined} />
            Refresh
          </button>
          {definition.create && (
            <button type="button" onClick={() => openEditor(null)} disabled={busy}>
              <Plus size={16} aria-hidden="true" />
              Add {definition.singular}
            </button>
          )}
        </div>
      </header>

      {definition.searchable !== false && (
        <form className="entity-search" onSubmit={submitSearch}>
          <input
            type="search"
            aria-label={`Search ${definition.plural.toLowerCase()}`}
            placeholder={`Search ${definition.plural.toLowerCase()} by ID or keyword...`}
            value={searchValue}
            onChange={event => setSearchValue(event.target.value)}
          />
          <button type="submit" className="secondary">
            <Search size={16} aria-hidden="true" />
            Search
          </button>
        </form>
      )}

      {editor && (
        <ModalDialog
          className="entity-editor-dialog"
          labelledBy="entity-editor-title"
          onClose={() => {
            if (!busy) setEditor(null);
          }}
        >
          <article>
            <header>
              <h3 id="entity-editor-title">
                {editor.mode === 'create' ? `New ${definition.singular}` : `Edit ${definition.singular}`}
              </h3>
              <button type="button" className="secondary" aria-label="Close editor"
                onClick={() => setEditor(null)} disabled={busy}><X size={16} aria-hidden="true" /></button>
            </header>
            <form onSubmit={save}>
              <div className="entity-form-grid">
                {fields.map(field => (
                  field.type === 'checkbox' ? (
                    <label key={field.name} className="entity-checkbox-field">
                      <input
                        type="checkbox"
                        checked={Boolean(editor.value[field.name])}
                        required={field.required}
                        onChange={event => setEditor(current => ({
                          ...current,
                          value: { ...current.value, [field.name]: event.target.checked },
                        }))}
                      />
                      <span>{field.label}</span>
                    </label>
                  ) : (
                    <label key={field.name}>
                      {field.label}
                      {field.type === 'select-number' || field.type === 'select' ? (
                        <select
                          value={editor.value[field.name] ?? ''}
                          required={field.required}
                          disabled={Boolean(optionsLoading[field.name]) || Boolean(field.dependsOn?.some(
                            dependency => !editor.value[dependency],
                          ))}
                          onChange={event => handleSelectChange(field, event.target.value)}
                        >
                          <option value="">Select...</option>
                          {(fieldOptions[field.name] ?? []).map(option => (
                            <option key={option.value} value={option.value}>{option.label}</option>
                          ))}
                        </select>
                      ) : (
                        <input
                          type={field.type ?? 'text'}
                          value={editor.value[field.name] ?? ''}
                          required={field.required}
                          min={field.type === 'number' ? field.min : undefined}
                          step={field.step}
                          onChange={event => setEditor(current => ({
                            ...current,
                            value: { ...current.value, [field.name]: event.target.value },
                          }))}
                        />
                      )}
                    </label>
                  )
                ))}
              </div>
              {formError && <p className="error" role="alert">{formError}</p>}
              <footer className="entity-editor-actions">
                <button type="submit" disabled={busy}>
                  {busy ? <LoaderCircle size={16} aria-hidden="true" className="icon-spin" /> : <Save size={16} aria-hidden="true" />}
                  {busy ? 'Saving...' : 'Save'}
                </button>
                <button type="button" className="secondary" onClick={() => setEditor(null)} disabled={busy}>
                  <X size={16} aria-hidden="true" />
                  Cancel
                </button>
              </footer>
            </form>
          </article>
        </ModalDialog>
      )}

      {deleteTarget && (
        <ModalDialog
          className="entity-delete-dialog"
          labelledBy="entity-delete-title"
          onClose={() => {
            if (!deleteBusy) setDeleteTarget(null);
          }}
        >
          <article>
            <header>
              <h3 id="entity-delete-title">Delete {definition.singular.toLowerCase()}?</h3>
            </header>
            <p>
              Delete “{deleteTarget[definition.nameField ?? 'name'] ?? definition.singular.toLowerCase()}”?
              This action cannot be undone.
            </p>
            <footer className="entity-editor-actions">
              <button type="button" className="contrast" onClick={remove} disabled={deleteBusy}>
                {deleteBusy ? <LoaderCircle size={16} aria-hidden="true" className="icon-spin" /> : <Trash2 size={16} aria-hidden="true" />}
                {deleteBusy ? 'Deleting...' : 'Delete'}
              </button>
              <button type="button" className="secondary" onClick={() => setDeleteTarget(null)}
                disabled={deleteBusy}>
                <X size={16} aria-hidden="true" />
                Cancel
              </button>
            </footer>
          </article>
        </ModalDialog>
      )}

      {error && <p className="error" role="alert">{error}</p>}
      {loading ? (
        <p aria-live="polite" aria-busy="true">Loading {definition.plural.toLowerCase()}...</p>
      ) : (
        <div className="admin-table-wrap">
          <table className="admin-table">
            <thead>
              <tr>
                {definition.columns.map(column => <th key={column.name}>{column.label}</th>)}
                <th><span className="visually-hidden">Actions</span></th>
              </tr>
            </thead>
            <tbody>
              {items.length === 0 ? (
                <tr><td colSpan={definition.columns.length + 1} className="empty-state">
                  No {definition.plural.toLowerCase()} found.
                </td></tr>
              ) : items.map((item, index) => (
                <tr key={item[definition.idField ?? 'id'] ?? index}>
                  {definition.columns.map(column => (
                    <td key={column.name}>{displayValue(item[column.name], column)}</td>
                  ))}
                  <td className="admin-actions">
                    {definition.update && (
                      <button type="button" className="secondary" onClick={() => openEditor(item)} disabled={busy}>
                        <Pencil size={15} aria-hidden="true" />
                        Edit
                      </button>
                    )}
                    {definition.remove && (
                      <button type="button" className="contrast" onClick={() => setDeleteTarget(item)}>
                        <Trash2 size={15} aria-hidden="true" />
                        Delete
                      </button>
                    )}
                    {definition.rowLink && (
                      <Link className="button secondary" to={definition.rowLink(item)}>
                        <ShieldCogCorner size={15} aria-hidden="true"/>
                        {definition.rowLinkLabel ?? 'Open'}
                      </Link>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {!loading && totalPages > 1 && (
        <nav className="admin-pagination" aria-label={`${definition.plural} pages`}>
          <button
            type="button"
            className="secondary"
            disabled={page <= 1}
            onClick={() => refresh(page - 1)}
          >
            <ChevronLeft size={16} aria-hidden="true" />
            Previous
          </button>
          <span>Page {page} of {totalPages} · {totalCount} total</span>
          <button
            type="button"
            className="secondary"
            disabled={page >= totalPages}
            onClick={() => refresh(page + 1)}
          >
            Next
            <ChevronRight size={16} aria-hidden="true" />
          </button>
        </nav>
      )}
    </section>
  );
}
