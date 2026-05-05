import { Injectable } from '@angular/core';
import { AuthResponse } from '../models/auth.models';

const ACCESS_TOKEN_KEY = 'events_access_token';
const REFRESH_TOKEN_KEY = 'events_refresh_token';
const ROLE_KEY = 'events_role';

@Injectable({ providedIn: 'root' })
export class TokenStorageService {
  setSession(response: AuthResponse) {
    localStorage.setItem(ACCESS_TOKEN_KEY, response.accessToken);
    localStorage.setItem(REFRESH_TOKEN_KEY, response.refreshToken);
    localStorage.setItem(ROLE_KEY, response.role);
  }

  clearSession() {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    localStorage.removeItem(REFRESH_TOKEN_KEY);
    localStorage.removeItem(ROLE_KEY);
  }

  getAccessToken() {
    return localStorage.getItem(ACCESS_TOKEN_KEY);
  }

  getRefreshToken() {
    return localStorage.getItem(REFRESH_TOKEN_KEY);
  }

  getRole() {
    return localStorage.getItem(ROLE_KEY);
  }
}
