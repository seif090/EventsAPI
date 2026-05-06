import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { OrdersService } from '../../core/services/orders.service';
import { TokenStorageService } from '../../core/services/token-storage.service';
import { Order } from '../../core/models/order.models';
import { LoadingComponent } from '../../shared/components/loading.component';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LoadingComponent],
  template: `
    <h2 class="mb-3">Orders</h2>

    <form class="card card-body mb-4 shadow-sm" [formGroup]="form" (ngSubmit)="createOrder()">
      <div class="row g-3">
        <div class="col-md-6">
          <label class="form-label">Booking ID</label>
          <input class="form-control" formControlName="bookingId" />
        </div>
        <div class="col-md-6">
          <label class="form-label">Client ID</label>
          <input class="form-control" formControlName="clientId" />
        </div>
        <div class="col-md-6">
          <label class="form-label">Album Type ID</label>
          <input class="form-control" formControlName="albumTypeId" />
        </div>
        <div class="col-md-6">
          <label class="form-label">Box Type ID (optional)</label>
          <input class="form-control" formControlName="boxTypeId" />
        </div>
        <div class="col-12">
          <label class="form-label">Photo IDs (comma separated)</label>
          <input class="form-control" formControlName="photoIds" />
        </div>
        <div class="col-12">
          <button class="btn btn-primary" [disabled]="form.invalid">Create Order</button>
        </div>
      </div>
    </form>

    <app-loading [loading]="loading"></app-loading>

    <div class="table-responsive" *ngIf="!loading">
      <table class="table table-striped align-middle">
        <thead>
          <tr>
            <th>ID</th>
            <th>Status</th>
            <th>Total</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let order of orders">
            <td>{{ order.id }}</td>
            <td>
              <span class="badge bg-secondary" [ngClass]="{
                'bg-warning': order.status === 'Pending',
                'bg-info': order.status === 'InProduction' || order.status === 'Shipped',
                'bg-success': order.status === 'Paid' || order.status === 'Delivered',
                'bg-danger': order.status === 'Cancelled'
              }">
                {{ order.status }}
              </span>
            </td>
            <td>{{ order.totalPrice | currency }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  `
})
export class OrdersComponent {
  orders: Order[] = [];
  loading = false;

  form = this.fb.group({
    bookingId: ['', Validators.required],
    clientId: ['', Validators.required],
    albumTypeId: ['', Validators.required],
    boxTypeId: [''],
    photoIds: ['', Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private ordersService: OrdersService,
    private tokenStorage: TokenStorageService
  ) {
    const userId = this.tokenStorage.getUserId();
    if (userId) {
      this.form.patchValue({ clientId: userId });
    }
    this.loadOrders();
  }

  loadOrders() {
    this.loading = true;
    this.ordersService.getOrders().subscribe({
      next: response => {
        this.orders = response.data?.items ?? [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  createOrder() {
    if (this.form.invalid) {
      return;
    }

    const payload = {
      ...this.form.getRawValue(),
      photoIds: (this.form.value.photoIds ?? '')
        .split(',')
        .map(value => value.trim())
        .filter(value => value)
    };

    this.ordersService.createOrder(payload).subscribe({
      next: () => {
        this.form.reset();
        this.loadOrders();
      }
    });
  }
}
