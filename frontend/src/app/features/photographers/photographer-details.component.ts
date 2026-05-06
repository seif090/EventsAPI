import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PhotographersService } from '../../core/services/photographers.service';
import { ReviewsService } from '../../core/services/reviews.service';
import { Photographer, PhotographerPortfolioItem } from '../../core/models/photographer.models';
import { Review } from '../../core/models/review.models';
import { LoadingComponent } from '../../shared/components/loading.component';

@Component({
  selector: 'app-photographer-details',
  standalone: true,
  imports: [CommonModule, LoadingComponent, RouterLink],
  template: `
    <app-loading [loading]="loading"></app-loading>

    <div *ngIf="!loading && photographer" class="row g-4">
      <div class="col-md-8">
        <h2>Photographer Details</h2>
        <p class="text-muted">{{ photographer.bio || 'Professional photographer.' }}</p>
        <p>Location: {{ photographer.location || 'Cairo' }}</p>
        <p>Price per hour: {{ photographer.pricePerHour | currency }}</p>
        <a class="btn btn-primary" routerLink="/bookings">Book now</a>
      </div>
      <div class="col-md-4">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Rating</h5>
            <p class="mb-0">{{ photographer.ratingAverage }} ({{ photographer.ratingCount }} reviews)</p>
          </div>
        </div>
      </div>
    </div>

    <div class="mt-4" *ngIf="portfolio.length">
      <h4>Portfolio</h4>
      <div class="row g-3">
        <div class="col-md-4" *ngFor="let item of portfolio">
          <div class="card h-100">
            <img [src]="item.imageUrl" class="card-img-top" alt="portfolio" />
            <div class="card-body">
              <h6 class="card-title">{{ item.title }}</h6>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="mt-4" *ngIf="reviews.length">
      <h4>Reviews</h4>
      <div class="list-group">
        <div class="list-group-item" *ngFor="let review of reviews">
          <strong>{{ review.rating }}/5</strong> - {{ review.comment || 'Great experience.' }}
        </div>
      </div>
    </div>
  `
})
export class PhotographerDetailsComponent {
  photographer?: Photographer;
  portfolio: PhotographerPortfolioItem[] = [];
  reviews: Review[] = [];
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private photographers: PhotographersService,
    private reviewsService: ReviewsService
  ) {
    const id = this.route.snapshot.paramMap.get('id') ?? '';
    this.load(id);
  }

  private load(id: string) {
    this.loading = true;
    this.photographers.getPhotographer(id).subscribe({
      next: response => {
        this.photographer = response.data ?? undefined;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });

    this.photographers.getPortfolio(id).subscribe({
      next: response => {
        this.portfolio = response.data ?? [];
      }
    });

    this.reviewsService.getReviewsByPhotographer(id).subscribe({
      next: response => {
        this.reviews = response.data?.items ?? [];
      }
    });
  }
}
