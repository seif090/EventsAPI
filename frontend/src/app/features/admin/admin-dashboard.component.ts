import { Component } from '@angular/core';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  template: `
    <h2 class="mb-3">Admin Dashboard</h2>
    <div class="row g-3">
      <div class="col-md-4">
        <div class="card h-100">
          <div class="card-body">
            <h5 class="card-title">Users</h5>
            <p class="card-text text-muted">Manage users and roles.</p>
          </div>
        </div>
      </div>
      <div class="col-md-4">
        <div class="card h-100">
          <div class="card-body">
            <h5 class="card-title">Bookings</h5>
            <p class="card-text text-muted">Approve and monitor bookings.</p>
          </div>
        </div>
      </div>
      <div class="col-md-4">
        <div class="card h-100">
          <div class="card-body">
            <h5 class="card-title">Photographers</h5>
            <p class="card-text text-muted">Verify photographer profiles.</p>
          </div>
        </div>
      </div>
    </div>
  `
})
export class AdminDashboardComponent {}
