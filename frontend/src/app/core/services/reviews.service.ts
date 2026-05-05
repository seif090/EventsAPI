import { Injectable } from '@angular/core';
import { ApiClientService } from './api-client.service';
import { ApiResponse } from '../models/api-response';
import { Review } from '../models/review.models';
import { PagedResult } from '../models/paged-result';

@Injectable({ providedIn: 'root' })
export class ReviewsService {
  constructor(private api: ApiClientService) {}

  getReviewsByPhotographer(photographerId: string, page = 1, pageSize = 20) {
    const params = { page, pageSize } as any;
    return this.api.get<ApiResponse<PagedResult<Review>>>(`reviews/by-photographer/${photographerId}`, { params });
  }

  createReview(payload: Omit<Review, 'id'>) {
    return this.api.post<ApiResponse<Review>>('reviews', payload);
  }
}
