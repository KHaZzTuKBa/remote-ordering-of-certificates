import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { Request } from '../../../models/request.model';
import { RequestStatus } from '../../../models/request-status.enum';
import { HeaderComponent } from '../../../components/header/header.component';

@Component({
  selector: 'app-requests-list',
  standalone: true,
  imports: [CommonModule, RouterLink, HeaderComponent],
  template: `
    <app-header [showBackButton]="true" [showProfileButton]="true"></app-header>
    <div class="page-container">
      <div class="card">
        <h1 style="margin-bottom: 30px; color: var(--dark-blue); font-size: 28px; text-align: center;">Мои заявки</h1>
        
        <div *ngIf="errorMessage" class="error-message">
          {{ errorMessage }}
        </div>

        <div *ngIf="isLoading" style="text-align: center; padding: 40px;">
          Загрузка...
        </div>

        <div *ngIf="!isLoading && requests.length === 0 && !errorMessage" style="text-align: center; padding: 40px; color: #666;">
          У вас пока нет заявок
        </div>

        <div *ngIf="!isLoading && requests.length > 0">
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
export class RequestsListComponent implements OnInit {
  requests: Request[] = [];
  isLoading: boolean = false;
  errorMessage: string = '';
  studentId: number = 1; // TODO: Получать из контекста пользователя

  constructor(
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadRequests();
  }

  loadRequests() {
    this.isLoading = true;
    this.errorMessage = '';

    this.userService.getRequestList({ studentId: this.studentId }).subscribe({
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
    this.router.navigate(['/student/requests', id]);
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

