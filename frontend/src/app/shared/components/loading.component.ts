import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-loading',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="text-center py-4" *ngIf="loading">
      <div class="spinner-border text-primary" role="status"></div>
    </div>
  `
})
export class LoadingComponent {
  @Input() loading = false;
}
