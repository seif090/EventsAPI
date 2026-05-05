export interface Review {
  id: string;
  bookingId: string;
  photographerId: string;
  clientId: string;
  rating: number;
  comment?: string;
}
