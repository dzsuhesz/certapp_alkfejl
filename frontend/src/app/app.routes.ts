import { Routes } from '@angular/router';
import { RootCertificatesComponent } from './components/root-certificates/root-certificates';
import { UserCertificatesComponent } from './components/user-certificates/user-certificates';

export const routes: Routes = [
  { path: '', redirectTo: 'root-certificates', pathMatch: 'full' },
  { path: 'root-certificates', component: RootCertificatesComponent },
  { path: 'user-certificates', component: UserCertificatesComponent }
];