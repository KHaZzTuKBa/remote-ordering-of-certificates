import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HeaderComponent } from '../../components/header/header.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [RouterLink, HeaderComponent],
  template: `
    <app-header [showBackButton]="true" [showProfileButton]="false"></app-header>
    <div class="page-container">
      <div class="card text-center">
        <h1 style="margin-bottom: 30px; color: var(--dark-blue); font-size: 28px;">Личный кабинет</h1>
        <p style="color: #666; margin-bottom: 30px;">Страница находится в разработке</p>
        <a routerLink="/" class="btn btn-primary">Вернуться на главную</a>
      </div>
    </div>
  `,
  styles: []
})
export class ProfileComponent {
}

