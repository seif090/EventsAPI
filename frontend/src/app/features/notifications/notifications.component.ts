import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NotificationsService } from '../../core/services/notifications.service';
import { Notification } from '../../core/models/notification.models';
import { LoadingComponent } from '../../shared/components/loading.component';
import { TokenStorageService } from '../../core/services/token-storage.service';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [CommonModule, LoadingComponent],
  template: `
    <h2 class="mb-3">Notifications</h2>
    <app-loading [loading]="loading"></app-loading>

    <div class="list-group" *ngIf="!loading && notifications.length">
      <div class="list-group-item d-flex justify-content-between align-items-start" *ngFor="let item of notifications">
        <div>
          <h6 class="mb-1">{{ item.title }}</h6>
          <p class="mb-1 text-muted">{{ item.message }}</p>
          <small>{{ item.createdAt | date: 'short' }}</small>
        </div>
        <button class="btn btn-sm btn-outline-primary" (click)="markRead(item)" [disabled]="item.isRead">
          {{ item.isRead ? 'Read' : 'Mark read' }}
        </button>
      </div>
    </div>
    <div class="text-muted" *ngIf="!loading && !notifications.length">No notifications yet.</div>
  `
})
export class NotificationsComponent {
  notifications: Notification[] = [];
  loading = false;

  constructor(
    private notificationsService: NotificationsService,
    private tokenStorage: TokenStorageService
  ) {
    this.load();
  }

  load() {
    const userId = this.tokenStorage.getUserId();
    if (!userId) {
      return;
    }

    this.loading = true;
    this.notificationsService.getUserNotifications(userId).subscribe({
      next: response => {
        this.notifications = response.data?.items ?? [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  markRead(item: Notification) {
    this.notificationsService.markRead(item.id).subscribe({
      next: () => {
        item.isRead = true;
      }
    });
  }
}
