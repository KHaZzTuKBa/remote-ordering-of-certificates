import { Component, Input } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Location } from "@angular/common";
import { RouterLink } from "@angular/router";

@Component({
    selector: "app-header",
    standalone: true,
    imports: [CommonModule, RouterLink],
    template: `
        <header class="app-header">
            <div class="header-content">
                <!-- Стрелка назад (только если showBackButton = true) -->
                <button
                    *ngIf="showBackButton"
                    class="header-icon-btn"
                    (click)="goBack()"
                    aria-label="Назад"
                >
                    <svg
                        width="24"
                        height="24"
                        viewBox="0 0 24 24"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                    >
                        <path
                            d="M15 18L9 12L15 6"
                            stroke="white"
                            stroke-width="2"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                    </svg>
                </button>
                <div *ngIf="!showBackButton" class="header-spacer"></div>

                <!-- Логотип в центре -->
                <div class="header-logo">
                    <img
                        src="assets/images/logo.svg"
                        alt="Удаленный заказ справок"
                        class="logo-image"
                    />
                </div>

                <!-- Иконка личного кабинета (только если showProfileButton = true) -->
                <a
                    *ngIf="showProfileButton"
                    routerLink="/profile"
                    class="header-icon-btn"
                    aria-label="Личный кабинет"
                >
                    <svg
                        width="24"
                        height="24"
                        viewBox="0 0 24 24"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                    >
                        <path
                            d="M20 21V19C20 17.9391 19.5786 16.9217 18.8284 16.1716C18.0783 15.4214 17.0609 15 16 15H8C6.93913 15 5.92172 15.4214 5.17157 16.1716C4.42143 16.9217 4 17.9391 4 19V21"
                            stroke="white"
                            stroke-width="2"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                        <circle
                            cx="12"
                            cy="7"
                            r="4"
                            stroke="white"
                            stroke-width="2"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                    </svg>
                </a>
                <div *ngIf="!showProfileButton" class="header-spacer"></div>
            </div>
        </header>
    `,
    styles: [
        `
            .app-header {
                background-color: var(--dark-blue);
                color: white;
                padding: 0;
                box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                position: sticky;
                top: 0;
                z-index: 1000;
            }

            .header-content {
                max-width: 1200px;
                margin: 0 auto;
                display: flex;
                align-items: center;
                justify-content: space-between;
                padding: 16px 20px;
                height: 64px;
            }

            .header-logo {
                flex: 1;
                display: flex;
                justify-content: center;
                align-items: center;
            }

            .logo-image {
                max-height: 48px;
                max-width: 200px;
                height: auto;
                width: auto;
                object-fit: contain;
            }

            .header-icon-btn {
                width: 40px;
                height: 40px;
                display: flex;
                align-items: center;
                justify-content: center;
                background: transparent;
                border: none;
                cursor: pointer;
                border-radius: 50%;
                transition: background-color 0.3s ease;
                color: white;
                text-decoration: none;
                flex-shrink: 0;
            }

            .header-icon-btn:hover {
                background-color: rgba(255, 255, 255, 0.1);
            }

            .header-icon-btn:active {
                background-color: rgba(255, 255, 255, 0.2);
            }

            .header-spacer {
                width: 40px;
                flex-shrink: 0;
            }

            @media (max-width: 768px) {
                .logo-image {
                    max-height: 40px;
                    max-width: 150px;
                }

                .header-content {
                    padding: 12px 16px;
                    height: 56px;
                }
            }
        `,
    ],
})
export class HeaderComponent {
    @Input() showBackButton: boolean = false;
    @Input() showProfileButton: boolean = false;

    constructor(private location: Location) {}

    goBack(): void {
        this.location.back();
    }
}
