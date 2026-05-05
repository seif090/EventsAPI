import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ApiResponse } from '../models/api-response';
import { AuthResponse, LoginRequest, RegisterRequest } from '../models/auth.models';
import { ApiClientService } from './api-client.service';
import { TokenStorageService } from './token-storage.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(
    private api: ApiClientService,
    private tokenStorage: TokenStorageService
  ) {}

  login(request: LoginRequest) {
    return this.api.post<ApiResponse<AuthResponse>>('auth/login', request).pipe(
      map(response => {
        if (!response.success || !response.data) {
          throw new Error(response.errors?.[0] ?? 'LoginFailed');
        }
        this.tokenStorage.setSession(response.data);
        return response.data;
      })
    );
  }

  register(request: RegisterRequest) {
    return this.api.post<ApiResponse<AuthResponse>>('auth/register', request).pipe(
      map(response => {
        if (!response.success || !response.data) {
          throw new Error(response.errors?.[0] ?? 'RegisterFailed');
        }
        this.tokenStorage.setSession(response.data);
        return response.data;
      })
    );
  }

  logout() {
    this.tokenStorage.clearSession();
  }
}
