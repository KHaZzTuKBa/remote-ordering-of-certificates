import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HeaderComponent } from '../../../components/header/header.component';

@Component({
  selector: 'app-student-dashboard',
  standalone: true,
  imports: [RouterLink, HeaderComponent],
  template: `
    <app-header [showBackButton]="true" [showProfileButton]="true"></app-header>
    <div class="page-container">
      <div class="card text-center">
        <h1 style="margin-bottom: 40px; color: var(--dark-blue); font-size: 28px;">Панель обучающегося</h1>
        <div style="display: flex; flex-direction: column; gap: 20px; align-items: center;">
          <a routerLink="/student/new-request" class="btn btn-primary btn-large">
            Заказать новую справку
          </a>
          <a routerLink="/student/requests" class="btn btn-secondary btn-large">
            Мои заявки
          </a>
        </div>
      </div>
    </div>
  `,
  styles: []
})
export class StudentDashboardComponent {
}

