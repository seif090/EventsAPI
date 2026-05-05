import { UserRole } from './user-role';

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber?: string;
  role: UserRole;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  userId: string;
  email: string;
  role: UserRole;
  accessToken: string;
  accessTokenExpiresAt: string;
  refreshToken: string;
}
