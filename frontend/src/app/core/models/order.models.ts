import { OrderStatus } from './enums';

export interface Order {
  id: string;
  bookingId: string;
  clientId: string;
  albumTypeId: string;
  boxTypeId?: string;
  status: OrderStatus;
  totalPrice: number;
}

export interface CreateOrderRequest {
  bookingId: string;
  clientId: string;
  albumTypeId: string;
  boxTypeId?: string;
  photoIds: string[];
}
