import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

import { ClientEndpoint } from '../environments/client-endpoint';
import { MaterialModule } from '../modules/material.module';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, MaterialModule],
  template: `
    <mat-toolbar>
      <!-- Home button -->
      <button mat-raised-button (click)="navigateTo('home')">
        Home
      </button>
      <span class="spacer"></span>
      <!-- Conditional Menus -->
      @if (isLoggedIn) {
        <button mat-button [matMenuTriggerFor]="workers">Employees</button>
        <mat-menu #workers="matMenu">
          <button mat-menu-item (click)="navigateTo(employeeAdd)">Add</button>
          <button mat-menu-item (click)="navigateTo(employeeList)">List</button>
        </mat-menu>
      }

      <!-- current user options -->
      <button mat-flat-button [matMenuTriggerFor]="profile">User</button>
      <mat-menu #profile="matMenu">
        <button mat-menu-item>Edit profile</button>
        @if (!isLoggedIn) {
          <button mat-menu-item (click)="navigateTo('user/login')">Login</button>
        } @else {
          <button mat-menu-item (click)="logout()">Logout</button>
        }

      </mat-menu>

    </mat-toolbar>
  `,
  styles: [
    `
      .spacer {
        flex: 1 1 auto;
      }
    `,
  ],
})
export class NavbarComponent {
  isLoggedIn: boolean = false;

  employeeAdd = ClientEndpoint.employeeAdd;
  employeeList = ClientEndpoint.employeeList;

  private _authService = inject(AuthenticationService);
  private _router = inject(Router);

  ngOnInit() {
    this.isLoggedIn = this._authService.isLoggedIn;
  }

  // getStatus() {
  //   this.isAuthenticated = this.authService.isAuthenticated();
  //   if (this.isAuthenticated) {
  //     console.log('User is authenticated');
  //     this.authService.isAdmin().subscribe({
  //       next: (response) => (this.isAdmin = response),
  //       error: (error) => console.error(error),
  //     });
  //   }
  // }

  navigateTo(path: string) {
    this._router.navigateByUrl(path);
  }
  
  logout() {
    this._authService.logout();
    //this.router.navigateByUrl(ClientUri.home());
  }
}
