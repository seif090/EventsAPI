import { Injectable } from '@angular/core';
import { ApiClientService } from './api-client.service';
import { ApiResponse } from '../models/api-response';
import { Photographer, PhotographerPortfolioItem } from '../models/photographer.models';

@Injectable({ providedIn: 'root' })
export class PhotographersService {
  constructor(private api: ApiClientService) {}

  getPhotographers(search?: string, location?: string) {
    const params = { search, location } as any;
    return this.api.get<ApiResponse<Photographer[]>>('photographers', { params });
  }

  getPhotographer(id: string) {
    return this.api.get<ApiResponse<Photographer>>(`photographers/${id}`);
  }

  getPortfolio(id: string) {
    return this.api.get<ApiResponse<PhotographerPortfolioItem[]>>(`photographers/${id}/portfolio`);
  }
}
