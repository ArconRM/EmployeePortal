import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'about',
    loadComponent: () => import('./about/about').then(m => m.AboutComponent)
  },
  {
    path: 'employees',
    loadChildren: () => import('./employees/employees.routes').then(m => m.EMPLOYEES_ROUTES)
  },
  {
    path: '',
    redirectTo: 'about',
    pathMatch: 'full'
  },
  {
    path: '**', // Страница не найдена
    redirectTo: 'about'
  }
];
