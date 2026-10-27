import { createContext, useContext, useMemo, useState } from 'react';
import { AuthenticationClient, LoginRequest } from '../../web-api-client.ts';
import { clearAccessToken, clearUserId, createApiClient, getAccessToken, getUserId, storeAccessToken, storeUserId } from '../api-client.js';

const AuthContext = createContext(null);
const authentication = createApiClient(AuthenticationClient);

export function AuthProvider({ children }) {
  const [accessToken, setAccessToken] = useState(() => getAccessToken());
  const [userId, setUserId] = useState(() => getUserId());
  const [isLoading] = useState(false);

  const value = useMemo(() => ({
    accessToken,
    isAuthenticated: Boolean(accessToken),
    isLoading,
    login: async (userId, password) => {
      const response = await authentication.login(new LoginRequest({ userId, password }));
      if (!response?.accessToken) throw new Error('The login response did not include an access token.');
      storeUserId(userId);
      setUserId(userId);
      storeAccessToken(response.accessToken);
      setAccessToken(response.accessToken);
    },
    logout: async () => {
      try {
        await authentication.logout({});
      } finally {
        clearUserId();
        setUserId(null);
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