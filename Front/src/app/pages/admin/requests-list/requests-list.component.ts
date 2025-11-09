import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from '../../../services/admin.service';
import { Request } from '../../../models/request.model';
import { RequestStatus } from '../../../models/request-status.enum';
import { HeaderComponent } from '../../../components/header/header.component';

@Component({
  selector: 'app-admin-requests-list',
  standalone: true,
  imports: [CommonModule, FormsModule, HeaderComponent],
  template: `
    <app-header [showBackButton]="true" [showProfileButton]="true"></app-header>
    <div class="page-container">
      <div class="card">
        <h1 style="margin-bottom: 30px; color: var(--dark-blue); font-size: 28px; text-align: center;">Поиск заявок</h1>
        
        <div *ngIf="errorMessage" class="error-message">
          {{ errorMessage }}
        </div>

        <form (ngSubmit)="onSearch()">
          <div class="form-group">
            <label class="form-label">Введите ID пользователя:</label>
            <input 
              type="number" 
              class="form-control" 
              [(ngModel)]="studentId" 
              name="studentId" 
              placeholder="Введите ID пользователя"
              required>
          </div>

          <button type="submit" class="btn btn-primary" [disabled]="isLoading" style="width: 100%;">
            {{ isLoading ? 'Поиск...' : 'Найти заявки' }}
          </button>
        </form>

        <div *ngIf="isLoading && studentId > 0" style="text-align: center; padding: 40px;">
          Загрузка...
        </div>

        <div *ngIf="!isLoading && requests.length === 0 && hasSearched && !errorMessage" style="text-align: center; padding: 40px; color: #666;">
          Заявки не найдены
        </div>

        <div *ngIf="!isLoading && requests.length > 0" style="margin-top: 30px;">
          <h2 style="margin-bottom: 20px; color: var(--dark-blue); font-size: 22px;">Найденные заявки:</h2>
          <div *ngFor="let request of requests" class="list-item" (click)="navigateToDetail(request.id)">
            <div class="list-item-title">Заявка #{{ request.id.substring(0, 8) }}</div>
            <div class="list-item-subtitle">
              <strong>Дата создания:</strong> {{ formatDate(request.date) }}<br>
              <strong>Статус:</strong> {{ getStatusText(request.requestStatus) }}
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: []
})
export class AdminRequestsListComponent {
  studentId: number = 0;
  requests: Request[] = [];
  isLoading: boolean = false;
  errorMessage: string = '';
  hasSearched: boolean = false;

  constructor(
    private adminService: AdminService,
    private router: Router
  ) {}

  onSearch() {
    if (!this.studentId || this.studentId <= 0) {
      this.errorMessage = 'Пожалуйста, введите корректный ID пользователя';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.hasSearched = true;

    this.adminService.getRequestList({ studentId: this.studentId }).subscribe({
      next: (response) => {
        this.requests = response.listOfRequests;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = error.message || 'Не удалось загрузить список заявок';
        this.isLoading = false;
      }
    });
  }

  navigateToDetail(id: string) {
    this.router.navigate(['/admin/request', id]);
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toLocaleDateString('ru-RU', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  getStatusText(status: RequestStatus): string {
    const statusMap: { [key: string]: string } = {
      'New': 'Новая',
      'Processing': 'В обработке',
      'Completed': 'Завершена',
      'Rejected': 'Отклонена'
    };
    return statusMap[status] || status;
  }
}

