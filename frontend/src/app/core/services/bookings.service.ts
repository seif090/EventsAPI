import { Injectable } from '@angular/core';
import { ApiClientService } from './api-client.service';
import { ApiResponse } from '../models/api-response';
import { Booking, CreateBookingRequest } from '../models/booking.models';
import { PagedResult } from '../models/paged-result';

@Injectable({ providedIn: 'root' })
export class BookingsService {
  constructor(private api: ApiClientService) {}

  getBookings(clientId?: string, photographerId?: string, page = 1, pageSize = 20) {
    const params = { clientId, photographerId, page, pageSize } as any;
    return this.api.get<ApiResponse<PagedResult<Booking>>>('bookings', { params });
  }

  getBooking(id: string) {
    return this.api.get<ApiResponse<Booking>>(`bookings/${id}`);
  }

  createBooking(request: CreateBookingRequest) {
    return this.api.post<ApiResponse<Booking>>('bookings', request);
  }

  updateStatus(id: string, status: string) {
    return this.api.put<ApiResponse<string>>(`bookings/${id}/status`, { status });
  }
}
