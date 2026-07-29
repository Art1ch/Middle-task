import { Routes } from '@angular/router';
import { DashboardComponent } from '../app/features/dashboard/dashboard';

export const routes: Routes = [
  {
    path: '',
    component: DashboardComponent
  },
  {
    path: '**',
    redirectTo: ''
  }
];