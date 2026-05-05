import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PhotosService } from '../../core/services/photos.service';
import { Photo } from '../../core/models/photo.models';
import { LoadingComponent } from '../../shared/components/loading.component';

@Component({
  selector: 'app-photos-selection',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LoadingComponent],
  template: `
    <h2 class="mb-3">Photos Selection</h2>

    <form class="row g-2 mb-3" [formGroup]="form" (ngSubmit)="load()">
      <div class="col-md-8">
        <input class="form-control" placeholder="Booking ID" formControlName="bookingId" />
      </div>
      <div class="col-md-4">
        <button class="btn btn-primary w-100" [disabled]="form.invalid">Load Photos</button>
      </div>
    </form>

    <app-loading [loading]="loading"></app-loading>

    <div class="row g-3" *ngIf="!loading">
      <div class="col-md-3" *ngFor="let photo of photos">
        <div class="card h-100">
          <img [src]="photo.url" class="card-img-top" alt="photo" />
          <div class="card-body">
            <div class="form-check">
              <input class="form-check-input" type="checkbox" [checked]="photo.isSelectedByClient" disabled />
              <label class="form-check-label">Selected</label>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class PhotosSelectionComponent {
  form = this.fb.group({
    bookingId: ['', Validators.required]
  });

  photos: Photo[] = [];
  loading = false;

  constructor(private fb: FormBuilder, private photosService: PhotosService) {}

  load() {
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.photosService.getPhotosByBooking(this.form.value.bookingId ?? '').subscribe({
      next: response => {
        this.photos = response.data ?? [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }
}
