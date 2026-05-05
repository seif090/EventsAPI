import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: true,
  template: `
    <div class="text-center">
      <h1 class="mb-3">Photographers Booking Platform</h1>
      <p class="text-muted">Browse photographers, book sessions, and order albums.</p>
    </div>
  `
})
export class HomeComponent {}
