import { Injectable } from '@angular/core';
import { ApiClientService } from './api-client.service';
import { ApiResponse } from '../models/api-response';
import { Photo, CreatePhotoRequest } from '../models/photo.models';

@Injectable({ providedIn: 'root' })
export class PhotosService {
  constructor(private api: ApiClientService) {}

  getPhotosByBooking(bookingId: string) {
    return this.api.get<ApiResponse<Photo[]>>(`photos/by-booking/${bookingId}`);
  }

  createPhoto(request: CreatePhotoRequest) {
    return this.api.post<ApiResponse<Photo>>('photos', request);
  }
}
