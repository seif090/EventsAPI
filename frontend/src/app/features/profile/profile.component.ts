import { Component } from '@angular/core';
import { TokenStorageService } from '../../core/services/token-storage.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  template: `
    <h2 class="mb-3">Profile</h2>
    <div class="row g-3">
      <div class="col-md-6">
        <div class="card shadow-sm">
          <div class="card-body">
            <h5 class="card-title">Account</h5>
            <p class="mb-1"><strong>Email:</strong> {{ email || 'N/A' }}</p>
            <p class="mb-1"><strong>User ID:</strong> {{ userId || 'N/A' }}</p>
            <p class="mb-0"><strong>Role:</strong> {{ role || 'N/A' }}</p>
          </div>
        </div>
      </div>
      <div class="col-md-6">
        <div class="card shadow-sm">
          <div class="card-body">
            <h5 class="card-title">Actions</h5>
            <button class="btn btn-outline-danger" (click)="logout()">Logout</button>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ProfileComponent {
  email = this.tokenStorage.getEmail();
  userId = this.tokenStorage.getUserId();
  role = this.tokenStorage.getRole();

  constructor(private tokenStorage: TokenStorageService) {}

  logout() {
    this.tokenStorage.clearSession();
  }
}
