const TOKEN_KEY = 'delex.accessToken';

export function createApiClient(ClientType) {
  return new ClientType(undefined, {
    fetch: (url, init = {}) => {
      const headers = new Headers(init.headers);
      const token = localStorage.getItem(TOKEN_KEY);
      if (token) headers.set('Authorization', `Bearer ${token}`);
      return window.fetch(url, { ...init, headers });
    },
  });
}

export function storeAccessToken(token) {
  localStorage.setItem(TOKEN_KEY, token);
}

export function getAccessToken() {
  return localStorage.getItem(TOKEN_KEY);
}

export function clearAccessToken() {
  localStorage.removeItem(TOKEN_KEY);
}
