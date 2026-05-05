export interface Photo {
  id: string;
  bookingId: string;
  photographerId: string;
  url: string;
  isSelectedByClient: boolean;
}

export interface CreatePhotoRequest {
  bookingId: string;
  photographerId: string;
  url: string;
  isSelectedByClient: boolean;
}
