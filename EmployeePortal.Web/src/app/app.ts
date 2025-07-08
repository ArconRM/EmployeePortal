import { Component, ChangeDetectionStrategy } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm">
      <div class="container-fluid">
        <a class="navbar-brand" routerLink="/">EmployeePortal</a>
        <div class="navbar-nav">
          <a class="nav-link" routerLink="/about" routerLinkActive="active">О компании</a>
          <a class="nav-link" routerLink="/employees" routerLinkActive="active">Сотрудники</a>
        </div>
      </div>
    </nav>
    <main class="container mt-4">
      <router-outlet></router-outlet>
    </main>
  `
})
export class AppComponent {}
