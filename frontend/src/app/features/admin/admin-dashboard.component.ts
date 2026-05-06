import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BookingsService } from '../../core/services/bookings.service';
import { OrdersService } from '../../core/services/orders.service';
import { Booking } from '../../core/models/booking.models';
import { Order } from '../../core/models/order.models';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <h2 class="mb-3">Admin Dashboard</h2>

    <div class="row g-3 mb-4">
      <div class="col-md-4">
        <div class="card shadow-sm">
          <div class="card-body">
            <h6 class="text-muted">Bookings</h6>
            <h3>{{ bookings.length }}</h3>
          </div>
        </div>
      </div>
      <div class="col-md-4">
        <div class="card shadow-sm">
          <div class="card-body">
            <h6 class="text-muted">Orders</h6>
            <h3>{{ orders.length }}</h3>
          </div>
        </div>
      </div>
      <div class="col-md-4">
        <div class="card shadow-sm">
          <div class="card-body">
            <h6 class="text-muted">Revenue</h6>
            <h3>{{ totalRevenue | currency }}</h3>
          </div>
        </div>
      </div>
    </div>

    <div class="row g-4">
      <div class="col-lg-6">
        <h5>Manage bookings</h5>
        <div class="table-responsive">
          <table class="table table-striped">
            <thead>
              <tr>
                <th>ID</th>
                <th>Status</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let booking of bookings">
                <td>{{ booking.id }}</td>
                <td>{{ booking.status }}</td>
                <td>
                  <select class="form-select form-select-sm" [value]="booking.status" (change)="updateBookingStatus(booking.id, $event.target.value)">
                    <option value="Pending">Pending</option>
                    <option value="Confirmed">Confirmed</option>
                    <option value="Completed">Completed</option>
                    <option value="Cancelled">Cancelled</option>
                  </select>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div class="col-lg-6">
        <h5>Manage orders</h5>
        <div class="table-responsive">
          <table class="table table-striped">
            <thead>
              <tr>
                <th>ID</th>
                <th>Status</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let order of orders">
                <td>{{ order.id }}</td>
                <td>{{ order.status }}</td>
                <td>
                  <select class="form-select form-select-sm" [value]="order.status" (change)="updateOrderStatus(order.id, $event.target.value)">
                    <option value="Pending">Pending</option>
                    <option value="Paid">Paid</option>
                    <option value="InProduction">InProduction</option>
                    <option value="Shipped">Shipped</option>
                    <option value="Delivered">Delivered</option>
                    <option value="Cancelled">Cancelled</option>
                  </select>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  `
})
export class AdminDashboardComponent {
  bookings: Booking[] = [];
  orders: Order[] = [];
  totalRevenue = 0;

  constructor(
    private bookingsService: BookingsService,
    private ordersService: OrdersService
  ) {
    this.loadData();
  }

  loadData() {
    this.bookingsService.getBookings().subscribe({
      next: response => {
        this.bookings = response.data?.items ?? [];
      }
    });

    this.ordersService.getOrders().subscribe({
      next: response => {
        this.orders = response.data?.items ?? [];
        this.totalRevenue = this.orders.reduce((sum, order) => sum + order.totalPrice, 0);
      }
    });
  }

  updateBookingStatus(id: string, status: string) {
    this.bookingsService.updateStatus(id, status).subscribe();
  }

  updateOrderStatus(id: string, status: string) {
    this.ordersService.updateStatus(id, status as any).subscribe();
  }
}
