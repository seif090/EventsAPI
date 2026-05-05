import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { BookingsService } from '../../core/services/bookings.service';
import { Booking } from '../../core/models/booking.models';
import { LoadingComponent } from '../../shared/components/loading.component';

@Component({
  selector: 'app-bookings',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LoadingComponent],
  template: `
    <h2 class="mb-3">Bookings</h2>

    <form class="card card-body mb-4" [formGroup]="form" (ngSubmit)="createBooking()">
      <div class="row g-3">
        <div class="col-md-6">
          <label class="form-label">Client ID</label>
          <input class="form-control" formControlName="clientId" />
        </div>
        <div class="col-md-6">
          <label class="form-label">Photographer ID</label>
          <input class="form-control" formControlName="photographerId" />
        </div>
        <div class="col-md-6">
          <label class="form-label">Scheduled At</label>
          <input class="form-control" type="datetime-local" formControlName="scheduledAt" />
        </div>
        <div class="col-md-3">
          <label class="form-label">Duration (min)</label>
          <input class="form-control" type="number" formControlName="durationMinutes" />
        </div>
        <div class="col-md-3">
          <label class="form-label">Price</label>
          <input class="form-control" type="number" formControlName="price" />
        </div>
        <div class="col-12">
          <label class="form-label">Notes</label>
          <textarea class="form-control" rows="2" formControlName="notes"></textarea>
        </div>
        <div class="col-12">
          <button class="btn btn-primary" [disabled]="form.invalid">Create Booking</button>
        </div>
      </div>
    </form>

    <app-loading [loading]="loading"></app-loading>

    <div class="table-responsive" *ngIf="!loading">
      <table class="table table-striped">
        <thead>
          <tr>
            <th>ID</th>
            <th>Photographer</th>
            <th>Scheduled</th>
            <th>Status</th>
            <th>Price</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let booking of bookings">
            <td>{{ booking.id }}</td>
            <td>{{ booking.photographerId }}</td>
            <td>{{ booking.scheduledAt | date: 'short' }}</td>
            <td>{{ booking.status }}</td>
            <td>{{ booking.price | currency }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  `
})
export class BookingsComponent {
  bookings: Booking[] = [];
  loading = false;

  form = this.fb.group({
    clientId: ['', Validators.required],
    photographerId: ['', Validators.required],
    scheduledAt: ['', Validators.required],
    durationMinutes: [60, [Validators.required, Validators.min(30)]],
    price: [0, [Validators.required, Validators.min(0)]],
    notes: ['']
  });

  constructor(private fb: FormBuilder, private bookingsService: BookingsService) {
    this.loadBookings();
  }

  loadBookings() {
    this.loading = true;
    this.bookingsService.getBookings().subscribe({
      next: response => {
        this.bookings = response.data?.items ?? [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  createBooking() {
    if (this.form.invalid) {
      return;
    }

    this.bookingsService.createBooking(this.form.getRawValue()).subscribe({
      next: () => {
        this.form.reset({ durationMinutes: 60, price: 0 });
        this.loadBookings();
      }
    });
  }
}
