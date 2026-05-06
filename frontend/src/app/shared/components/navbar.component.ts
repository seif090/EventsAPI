import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TokenStorageService } from '../../core/services/token-storage.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <nav class="navbar navbar-expand-lg navbar-light bg-white shadow-sm">
      <div class="container">
        <a class="navbar-brand fw-bold" routerLink="/">Events</a>
        <div class="collapse navbar-collapse show">
          <ul class="navbar-nav me-auto">
            <li class="nav-item"><a class="nav-link" routerLink="/photographers">Photographers</a></li>
            <li class="nav-item"><a class="nav-link" routerLink="/bookings">Bookings</a></li>
            <li class="nav-item"><a class="nav-link" routerLink="/orders">Orders</a></li>
            <li class="nav-item"><a class="nav-link" routerLink="/notifications" *ngIf="isAuthenticated">Notifications</a></li>
            <li class="nav-item"><a class="nav-link" routerLink="/admin" *ngIf="isAdmin">Admin</a></li>
          </ul>
          <div class="d-flex gap-2">
            <a class="btn btn-outline-primary" routerLink="/auth/login" *ngIf="!isAuthenticated">Login</a>
            <a class="btn btn-primary" routerLink="/auth/register" *ngIf="!isAuthenticated">Register</a>
            <a class="btn btn-outline-secondary" routerLink="/profile" *ngIf="isAuthenticated">Profile</a>
            <button class="btn btn-outline-secondary" (click)="logout()" *ngIf="isAuthenticated">Logout</button>
          </div>
        </div>
      </div>
    </nav>
  `
})
export class NavbarComponent {
  private tokenStorage = inject(TokenStorageService);

  get isAuthenticated() {
    return !!this.tokenStorage.getAccessToken();
  }

  get isAdmin() {
    return this.tokenStorage.getRole() === 'Admin';
  }

  logout() {
    this.tokenStorage.clearSession();
  }
}
