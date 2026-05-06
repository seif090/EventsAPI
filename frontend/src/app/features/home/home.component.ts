import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink],
  template: `
    <section class="bg-white p-5 rounded-4 shadow-sm mb-4">
      <div class="row align-items-center">
        <div class="col-md-7">
          <h1 class="mb-3">Book photographers, design albums, ship memories</h1>
          <p class="text-muted mb-4">
            Discover verified photographers, book sessions, select your photos, and order premium albums.
          </p>
          <a class="btn btn-primary me-2" routerLink="/photographers">Browse photographers</a>
          <a class="btn btn-outline-secondary" routerLink="/bookings">Create booking</a>
        </div>
        <div class="col-md-5 text-center">
          <div class="bg-light rounded-4 p-4">Modern Experiences</div>
        </div>
      </div>
    </section>

    <section class="row g-3">
      <div class="col-md-4">
        <div class="card h-100 shadow-sm">
          <div class="card-body">
            <h5>Curated photographers</h5>
            <p class="text-muted">Verified professionals with portfolios and ratings.</p>
          </div>
        </div>
      </div>
      <div class="col-md-4">
        <div class="card h-100 shadow-sm">
          <div class="card-body">
            <h5>Seamless booking</h5>
            <p class="text-muted">Pick dates, confirm availability, and manage sessions.</p>
          </div>
        </div>
      </div>
      <div class="col-md-4">
        <div class="card h-100 shadow-sm">
          <div class="card-body">
            <h5>Album production</h5>
            <p class="text-muted">Select photos, choose albums and shipping.</p>
          </div>
        </div>
      </div>
    </section>
  `
})
export class HomeComponent {}
