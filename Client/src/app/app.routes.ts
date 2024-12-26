import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
	/** Home */
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    loadComponent: () => import('./pages/home-page.component').then((m) => m.HomePageComponent),
  },
	/** Authentication and user management */
	{
    path: 'user',
    children: [
      {
        path: 'register',
        loadComponent: () => import('./pages/signup-page.component').then((m) => m.SignupPageComponent),
      },
      {
        path: 'login',
        loadComponent: () => import('./pages/login-page.component').then((m) => m.LoginPageComponent),
      },
    ],
  },
  {
    path: 'employee',
    children: [
      {
        path: 'add',
        loadComponent: () => import('./components/employee-add.component').then((m) => m.EmployeeAddComponent),
      },
      {
        path: 'list',
        loadComponent: () => import('./components/employee-list.component').then((m) => m.EmployeeListComponent),
      },
    ],
  },
	/** Example */
	// {
  //   path: 'URL_PARENT',
  //   children: [
  //     {
  //       path: ':id',
  //       pathMatch: 'full',
  //       loadComponent: () => import('COMPONENTPATH').then((m) => m.COMPONENTNAME),
  //       canActivate: [AuthGuard],
  //     },
	// 	]
	// },
	/** Errors */
	{
    path: '401',
    loadComponent: () => import('../app/errors/unauthorized-access.component').then((m) => m.UnauthorizedAccessComponent),
  },
  {
    path: '**',
    loadComponent: () => import('../app/errors/page-not-found.component').then((m) => m.PageNotFoundComponent),
  },
];
