import { BookingStatus } from './enums';

export interface Booking {
  id: string;
  clientId: string;
  photographerId: string;
  scheduledAt: string;
  durationMinutes: number;
  status: BookingStatus;
  notes?: string;
  price: number;
}

export interface CreateBookingRequest {
  clientId: string;
  photographerId: string;
  scheduledAt: string;
  durationMinutes: number;
  notes?: string;
  price: number;
}
