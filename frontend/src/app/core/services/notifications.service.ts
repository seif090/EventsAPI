import { Injectable } from '@angular/core';
import { ApiClientService } from './api-client.service';
import { ApiResponse } from '../models/api-response';
import { Notification } from '../models/notification.models';
import { PagedResult } from '../models/paged-result';

@Injectable({ providedIn: 'root' })
export class NotificationsService {
  constructor(private api: ApiClientService) {}

  getUserNotifications(userId: string, page = 1, pageSize = 20) {
    const params = { page, pageSize } as any;
    return this.api.get<ApiResponse<PagedResult<Notification>>>(`notifications/by-user/${userId}`, { params });
  }

  markRead(id: string) {
    return this.api.put<ApiResponse<string>>(`notifications/${id}/read`, {});
  }
}
