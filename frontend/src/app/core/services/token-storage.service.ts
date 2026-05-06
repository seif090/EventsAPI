import { Injectable } from '@angular/core';
import { AuthResponse } from '../models/auth.models';

const ACCESS_TOKEN_KEY = 'events_access_token';
const REFRESH_TOKEN_KEY = 'events_refresh_token';
const ROLE_KEY = 'events_role';
const EMAIL_KEY = 'events_email';
const USER_ID_KEY = 'events_user_id';

@Injectable({ providedIn: 'root' })
export class TokenStorageService {
  setSession(response: AuthResponse) {
    localStorage.setItem(ACCESS_TOKEN_KEY, response.accessToken);
    localStorage.setItem(REFRESH_TOKEN_KEY, response.refreshToken);
    localStorage.setItem(ROLE_KEY, response.role);
    localStorage.setItem(EMAIL_KEY, response.email);
    localStorage.setItem(USER_ID_KEY, response.userId);
  }

  clearSession() {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    localStorage.removeItem(REFRESH_TOKEN_KEY);
    localStorage.removeItem(ROLE_KEY);
    localStorage.removeItem(EMAIL_KEY);
    localStorage.removeItem(USER_ID_KEY);
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

  getEmail() {
    return localStorage.getItem(EMAIL_KEY);
  }

  getUserId() {
    return localStorage.getItem(USER_ID_KEY);
  }
}
