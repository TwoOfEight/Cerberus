import { Observable } from 'rxjs';

import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
    FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators
} from '@angular/forms';
import { Router } from '@angular/router';

import { ClientEndpoint } from '../environments/client-endpoint';
import { Navigator } from '../environments/navigator';
import { LoginRequest } from '../models/authentication';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <form [formGroup]="loginForm" novalidate (ngSubmit)="login()">
      <h2>Log in</h2>

      <!-- Email Field -->
      <div>
        <label for="username">username</label>
        <input id="username" type="username" formControlName="username" />
        <div *ngIf="loginForm.controls['username'].hasError('required')">
          <p>username is <strong>required</strong>.</p>
        </div>
      </div>

      <!-- Password Field -->
      <div>
        <label for="password">password</label>
        <input
          id="password"
          [type]="hidePassword ? 'password' : 'text'"
          formControlName="password"
        />
        <button type="button" (click)="hidePassword = !hidePassword">
          {{ hidePassword ? 'Show' : 'Hide' }}
        </button>
        <div *ngIf="loginForm.controls['password'].hasError('required')">
          <p>password is <strong>required</strong>.</p>
        </div>
      </div>

      <!-- Submit and Cancel Buttons -->
      <div>
        <button type="submit" [disabled]="loginForm.invalid">Login</button>
        <button type="button" (click)="cancel()">Cancel</button>
      </div>
    </form>
  `,
})
export class LoginComponent {
  private authenticationService = inject(AuthenticationService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  private navigator = inject(Navigator);

  public loginForm!: FormGroup;
  public hidePassword = true;

  expiration$?: Observable<Date>;

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.loginForm = this.formBuilder.group({
      username: new FormControl('', {
        validators: [Validators.required],
        nonNullable: true,
      }),
      password: new FormControl('', {
        validators: [Validators.required],
        nonNullable: true,
      }),
    });
  }

  login() {
    if (this.loginForm.valid) {
      const formData: LoginRequest = this.loginForm.value;
      this.authenticationService.login(formData).subscribe({
        next: () => {
          this.navigator.getHome(1000);
        },
        error: () => {
          this.notFound();
        },
      });
    }
  }

  cancel() {
    this.router.navigateByUrl(ClientEndpoint.home);
  }

  notFound() {
    this.router.navigateByUrl('/404');
  }
}
