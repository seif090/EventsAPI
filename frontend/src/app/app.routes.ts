import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
  },
  {
    path: 'photographers',
    canActivate: [authGuard],
    loadComponent: () => import('./features/photographers/photographers-list.component').then(m => m.PhotographersListComponent)
  },
  {
    path: 'photographers/:id',
    canActivate: [authGuard],
    loadComponent: () => import('./features/photographers/photographer-details.component').then(m => m.PhotographerDetailsComponent)
  },
  {
    path: 'bookings',
    canActivate: [authGuard],
    loadComponent: () => import('./features/bookings/bookings.component').then(m => m.BookingsComponent)
  },
  {
    path: 'photos',
    canActivate: [authGuard],
    loadComponent: () => import('./features/photos/photos-selection.component').then(m => m.PhotosSelectionComponent)
  },
  {
    path: 'orders',
    canActivate: [authGuard],
    loadComponent: () => import('./features/orders/orders.component').then(m => m.OrdersComponent)
  },
  {
    path: 'notifications',
    canActivate: [authGuard],
    loadComponent: () => import('./features/notifications/notifications.component').then(m => m.NotificationsComponent)
  },
  {
    path: 'profile',
    canActivate: [authGuard],
    loadComponent: () => import('./features/profile/profile.component').then(m => m.ProfileComponent)
  },
  {
    path: 'admin',
    canActivate: [authGuard, roleGuard(['Admin'])],
    loadComponent: () => import('./features/admin/admin-dashboard.component').then(m => m.AdminDashboardComponent)
  },
  {
    path: '',
    loadComponent: () => import('./features/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
