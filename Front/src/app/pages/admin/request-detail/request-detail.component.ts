import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AdminService } from '../../../services/admin.service';
import { RequestInformation } from '../../../models/request-information.model';
import { RequestStatus } from '../../../models/request-status.enum';
import { ReceivingFormat } from '../../../models/receiving-format.enum';
import { HeaderComponent } from '../../../components/header/header.component';

@Component({
  selector: 'app-admin-request-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, HeaderComponent],
  template: `
    <app-header [showBackButton]="true" [showProfileButton]="true"></app-header>
    <div class="page-container">
      <div class="card">
        <h1 style="margin-bottom: 30px; color: var(--dark-blue); font-size: 28px; text-align: center;">Детали заявки</h1>
        
        <div *ngIf="errorMessage" class="error-message">
          {{ errorMessage }}
        </div>

        <div *ngIf="isLoading" style="text-align: center; padding: 40px;">
          Загрузка...
        </div>

        <div *ngIf="!isLoading && requestInfo">
          <div class="detail-section">
            <div class="detail-label">ID заявки:</div>
            <div class="detail-value">{{ requestInfo.id }}</div>
          </div>

          <div class="detail-section">
            <div class="detail-label">ФИО:</div>
            <div class="detail-value">{{ requestInfo.name }}</div>
          </div>

          <div class="detail-section">
            <div class="detail-label">Дата создания:</div>
            <div class="detail-value">{{ formatDate(requestInfo.date) }}</div>
          </div>

          <div class="detail-section">
            <div class="detail-label">Статус:</div>
            <div class="detail-value">{{ getStatusText(requestInfo.fullRequestStatus) }}</div>
          </div>

          <div class="detail-section">
            <div class="detail-label">Формат получения:</div>
            <div class="detail-value">{{ getReceivingFormatText(requestInfo.receivingFormat) }}</div>
          </div>

          <div class="detail-section" *ngIf="requestInfo.filePath">
            <div class="detail-label">Путь к файлу:</div>
            <div class="detail-value">{{ requestInfo.filePath }}</div>
          </div>
        </div>

        <div style="margin-top: 30px; text-align: center;">
          <a routerLink="/admin/requests" class="btn btn-secondary">Вернуться к поиску</a>
        </div>
      </div>
    </div>
  `,
  styles: []
})
export class AdminRequestDetailComponent implements OnInit {
  requestInfo: RequestInformation | null = null;
  isLoading: boolean = false;
  errorMessage: string = '';

  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadRequest(id);
    } else {
      this.errorMessage = 'ID заявки не указан';
    }
  }

  loadRequest(id: string) {
    this.isLoading = true;
    this.errorMessage = '';

    this.adminService.getRequest({ requestID: id }).subscribe({
      next: (response) => {
        this.requestInfo = response.fullRequestInformation;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = error.message || 'Не удалось загрузить информацию о заявке';
        this.isLoading = false;
      }
    });
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

  getReceivingFormatText(format: ReceivingFormat): string {
    const formatMap: { [key: string]: string } = {
      'InPerson': 'Очно',
      'Remotely': 'Электронно'
    };
    return formatMap[format] || format;
  }
}

