import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { CreateRequestDTO } from '../../../models/create-request-dto.model';
import { HeaderComponent } from '../../../components/header/header.component';

@Component({
  selector: 'app-new-request',
  standalone: true,
  imports: [CommonModule, FormsModule, HeaderComponent],
  template: `
    <app-header [showBackButton]="true" [showProfileButton]="true"></app-header>
    <div class="page-container">
      <div class="card">
        <h1 style="margin-bottom: 30px; color: var(--dark-blue); font-size: 28px; text-align: center;">Заказ новой справки</h1>
        
        <div *ngIf="errorMessage" class="error-message">
          {{ errorMessage }}
        </div>

        <form (ngSubmit)="onSubmit()">
          <div class="form-group">
            <label class="form-label">Тип справки:</label>
            <div class="radio-group">
              <div class="radio-option">
                <input type="radio" id="certificate" name="certificateType" value="Справка об обучении" checked disabled>
                <label for="certificate">Справка об обучении</label>
              </div>
            </div>
          </div>

          <div class="form-group">
            <label class="form-label">Формат получения:</label>
            <div class="radio-group">
              <div class="radio-option">
                <input type="radio" id="offline" name="format" value="Очно" [(ngModel)]="format" required>
                <label for="offline">Очно</label>
              </div>
              <div class="radio-option">
                <input type="radio" id="online" name="format" value="Электронно" [(ngModel)]="format" required>
                <label for="online">Электронно</label>
              </div>
            </div>
          </div>

          <div class="form-group">
            <label class="form-label">ID студента:</label>
            <input type="number" class="form-control" [(ngModel)]="studentId" name="studentId" required>
          </div>

          <div class="form-group">
            <label class="form-label">ФИО:</label>
            <input type="text" class="form-control" [(ngModel)]="fullName" name="fullName" required>
          </div>

          <div class="form-group">
            <label class="form-label">Курс:</label>
            <input type="number" class="form-control" [(ngModel)]="course" name="course" required min="1" max="6">
          </div>

          <button type="submit" class="btn btn-primary" [disabled]="isSubmitting" style="width: 100%; margin-top: 20px;">
            {{ isSubmitting ? 'Отправка...' : 'Отправить заявку' }}
          </button>
        </form>
      </div>
    </div>
  `,
  styles: []
})
export class NewRequestComponent {
  format: string = 'Очно';
  studentId: number = 0;
  fullName: string = '';
  course: number = 1;
  isSubmitting: boolean = false;
  errorMessage: string = '';

  constructor(
    private userService: UserService,
    private router: Router
  ) {}

  onSubmit() {
    if (!this.format || !this.studentId || !this.fullName || !this.course) {
      this.errorMessage = 'Пожалуйста, заполните все поля';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    const dto: CreateRequestDTO = {
      studentId: this.studentId,
      fullName: this.fullName,
      course: this.course
    };

    this.userService.createRequest(dto).subscribe({
      next: (response) => {
        alert(response.message);
        this.router.navigate(['/student/requests']);
      },
      error: (error) => {
        this.errorMessage = error.message || 'Произошла ошибка при отправке заявки';
        this.isSubmitting = false;
      }
    });
  }
}

