import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HeaderComponent } from '../../components/header/header.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, HeaderComponent],
  template: `
    <app-header [showBackButton]="false" [showProfileButton]="false"></app-header>
    <div class="page-container">
      <div class="card text-center">
        <h1 style="margin-bottom: 40px; color: var(--dark-blue); font-size: 32px;">Удаленный заказ справок</h1>
        <div style="display: flex; flex-direction: column; gap: 20px; align-items: center;">
          <a routerLink="/student" class="btn btn-primary btn-large">
            Я обучающийся
          </a>
          <a routerLink="/admin/requests" class="btn btn-secondary btn-large">
            Я администратор
          </a>
        </div>
      </div>
    </div>
  `,
  styles: []
})
export class HomeComponent {
}

