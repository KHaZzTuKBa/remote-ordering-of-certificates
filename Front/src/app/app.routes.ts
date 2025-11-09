import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: 'student',
    loadComponent: () => import('./pages/student/student-dashboard/student-dashboard.component').then(m => m.StudentDashboardComponent)
  },
  {
    path: 'student/new-request',
    loadComponent: () => import('./pages/student/new-request/new-request.component').then(m => m.NewRequestComponent)
  },
  {
    path: 'student/requests',
    loadComponent: () => import('./pages/student/requests-list/requests-list.component').then(m => m.RequestsListComponent)
  },
  {
    path: 'student/requests/:id',
    loadComponent: () => import('./pages/student/request-detail/request-detail.component').then(m => m.RequestDetailComponent)
  },
  {
    path: 'admin/requests',
    loadComponent: () => import('./pages/admin/requests-list/requests-list.component').then(m => m.AdminRequestsListComponent)
  },
  {
    path: 'admin/request/:id',
    loadComponent: () => import('./pages/admin/request-detail/request-detail.component').then(m => m.AdminRequestDetailComponent)
  },
  {
    path: 'profile',
    loadComponent: () => import('./pages/profile/profile.component').then(m => m.ProfileComponent)
  }
];

