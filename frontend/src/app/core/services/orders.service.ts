import { Injectable } from '@angular/core';
import { ApiClientService } from './api-client.service';
import { ApiResponse } from '../models/api-response';
import { Order, CreateOrderRequest } from '../models/order.models';
import { PagedResult } from '../models/paged-result';
import { OrderStatus } from '../models/enums';

@Injectable({ providedIn: 'root' })
export class OrdersService {
  constructor(private api: ApiClientService) {}

  getOrders(clientId?: string, status?: OrderStatus, page = 1, pageSize = 20) {
    const params = { clientId, status, page, pageSize } as any;
    return this.api.get<ApiResponse<PagedResult<Order>>>('orders', { params });
  }

  getOrder(id: string) {
    return this.api.get<ApiResponse<Order>>(`orders/${id}`);
  }

  createOrder(request: CreateOrderRequest) {
    return this.api.post<ApiResponse<Order>>('orders', request);
  }

  updateStatus(id: string, status: OrderStatus) {
    return this.api.put<ApiResponse<string>>(`orders/${id}/status`, { status });
  }
}
