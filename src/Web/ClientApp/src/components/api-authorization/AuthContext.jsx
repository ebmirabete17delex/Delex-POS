import { createContext, useContext, useMemo, useState } from 'react';
import { AuthenticationClient, LoginRequest } from '../../web-api-client.ts';
import { clearAccessToken, createApiClient, getAccessToken, storeAccessToken } from '../api-client.js';

const AuthContext = createContext(null);
const authentication = createApiClient(AuthenticationClient);

export function AuthProvider({ children }) {
  const [accessToken, setAccessToken] = useState(() => getAccessToken());
  const [isLoading] = useState(false);

  const value = useMemo(() => ({
    accessToken,
    isAuthenticated: Boolean(accessToken),
    isLoading,
    login: async (userId, password) => {
      const response = await authentication.login(new LoginRequest({ userId, password }));
      if (!response?.accessToken) throw new Error('The login response did not include an access token.');
      storeAccessToken(response.accessToken);
      setAccessToken(response.accessToken);
    },
    logout: async () => {
      try {
        await authentication.logout({});
      } finally {
        clearAccessToken();
        setAccessToken(null);
      }
    },
  }), [accessToken, isLoading]);

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) throw new Error('useAuth must be used inside AuthProvider.');
  return context;
}