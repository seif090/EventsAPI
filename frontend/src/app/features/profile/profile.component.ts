import { Component } from '@angular/core';

@Component({
  selector: 'app-profile',
  standalone: true,
  template: `
    <h2 class="mb-3">Profile</h2>
    <div class="card">
      <div class="card-body">
        <p class="mb-1">Manage your profile details and preferences.</p>
        <p class="text-muted">This section will display user information and photographer dashboard.</p>
      </div>
    </div>
  `
})
export class ProfileComponent {}
