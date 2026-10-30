const TOKEN_KEY = 'delex.accessToken';
const USER_ID = 'delex.userId';

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

// TOKEN
export function storeAccessToken(token) {
  localStorage.setItem(TOKEN_KEY, token);
}

export function getAccessToken() {
  return localStorage.getItem(TOKEN_KEY);
}

export function clearAccessToken() {
  localStorage.removeItem(TOKEN_KEY);
}

// USER ID
export function storeUserId(id){
  localStorage.setItem(USER_ID, id);
}

export function getUserId() {
  return localStorage.getItem(USER_ID);
}

export function clearUserId() {
  localStorage.removeItem(USER_ID);
}
