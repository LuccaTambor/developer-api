import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
  {path: 'login', component: LoginComponent, title: 'Developer InBev Login'},
  {path: 'create-user', component: CreateUserComponent, title: 'Create new User'},
  {path: 'home', component: HomeComponent, title: 'Home'},
  {path: '', redirectTo: 'login',  pathMatch: 'full'}
];
