import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { PhotographersService } from '../../core/services/photographers.service';
import { Photographer } from '../../core/models/photographer.models';
import { LoadingComponent } from '../../shared/components/loading.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-photographers-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LoadingComponent, RouterLink],
  template: `
    <h2 class="mb-3">Photographers</h2>
    <form class="row g-2 mb-4" [formGroup]="filterForm" (ngSubmit)="load()">
      <div class="col-md-5">
        <input class="form-control" placeholder="Search" formControlName="search" />
      </div>
      <div class="col-md-5">
        <input class="form-control" placeholder="Location" formControlName="location" />
      </div>
      <div class="col-md-2">
        <button class="btn btn-primary w-100" type="submit">Filter</button>
      </div>
    </form>

    <app-loading [loading]="loading"></app-loading>

    <div class="row g-3" *ngIf="!loading">
      <div class="col-md-4" *ngFor="let photographer of photographers">
        <div class="card h-100 shadow border-primary">
          <div class="card-body">
            <h5 class="card-title">Photographer</h5>
            <p class="card-text text-muted">{{ photographer.location || 'Cairo' }}</p>
            <p class="mb-1">Price: {{ photographer.pricePerHour | currency }}</p>
            <p class="mb-2">Rating: {{ photographer.ratingAverage }} ({{ photographer.ratingCount }})</p>
            <a class="btn btn-outline-primary" [routerLink]="['/photographers', photographer.id]">View</a>
          </div>
        </div>
      </div>
    </div>
  `
})
export class PhotographersListComponent {
  filterForm = this.fb.group({
    search: [''],
    location: ['']
  });

  photographers: Photographer[] = [];
  loading = false;

  constructor(private fb: FormBuilder, private service: PhotographersService) {
    this.load();
  }

  load() {
    this.loading = true;
    const { search, location } = this.filterForm.value;
    this.service.getPhotographers(search ?? '', location ?? '').subscribe({
      next: response => {
        this.photographers = response.data ?? [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }
}
