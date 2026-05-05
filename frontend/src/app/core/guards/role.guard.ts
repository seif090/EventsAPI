import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { TokenStorageService } from '../services/token-storage.service';
import { UserRole } from '../models/user-role';

export const roleGuard = (allowed: UserRole[]): CanActivateFn => () => {
  const tokenStorage = inject(TokenStorageService);
  const router = inject(Router);
  const role = tokenStorage.getRole() as UserRole | null;

  if (role && allowed.includes(role)) {
    return true;
  }

  return router.parseUrl('/');
};
