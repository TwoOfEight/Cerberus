import { Component, EventEmitter, inject, Output } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Navigator } from '../environments/navigator';
import { ClientEndpoint } from '../environments/client-endpoint';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup',
  imports: [CommonModule ,ReactiveFormsModule],
  template: `
    <form [formGroup]="registerForm" novalidate (ngSubmit)="register()" class="form">
      <h2>Sign up</h2>

      <!-- Email Field -->
      <div>
        <label for="email">E-mail</label>
        <input id="email" type="email" formControlName="email" />
        <div *ngIf="registerForm.controls['email'].hasError('required')">
          <p>E-mail address is <strong>required</strong>.</p>
        </div>
      </div>

      <!-- Name Field -->
      <div>
        <label for="name">Name</label>
        <input id="name" type="text" formControlName="name" />
        <div *ngIf="registerForm.controls['name'].hasError('required')">
          <p>Name is <strong>required</strong>.</p>
        </div>
      </div>

      <!-- Password Field -->
      <div>
        <label for="password">Password</label>
        <input
          id="password"
          [type]="hidePassword ? 'password' : 'text'"
          formControlName="password"
        />
        <button type="button" (click)="hidePassword = !hidePassword">
          {{ hidePassword ? 'Show' : 'Hide' }}
        </button>
        <div *ngIf="registerForm.controls['password'].hasError('required')">
          <p>Password is <strong>required</strong>.</p>
        </div>
      </div>

      <!-- Submit and Cancel Buttons -->
      <div>
        <button type="submit" [disabled]="registerForm.invalid">Sign up</button>
        <button type="button" (click)="cancel()">Cancel</button>
      </div>
    </form>
  `,
})
export class RegisterComponent {
  private authService = inject(AuthenticationService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  private navigator = inject(Navigator);

  public registerForm!: FormGroup;
  public hidePassword = true;

  @Output()
  cancelRegister = new EventEmitter();

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.registerForm = this.formBuilder.group({
      email: new FormControl('', {
        validators: [Validators.required],
        nonNullable: true,
      }),
      name: new FormControl('', {
        validators: [Validators.required],
        nonNullable: true,
      }),
      password: new FormControl('', {
        validators: [Validators.required],
        nonNullable: true,
      }),
    });
  }

  register() {
    if (this.registerForm.valid) {
      const formData = this.registerForm.value;

      this.authService.register(formData).subscribe(
        (response) => {
          this.navigator.getHome(100);
        },
        (error) => {
          this.notFound();
        }
      );

      this.router.navigateByUrl(ClientEndpoint.Home());
    }
  }

  cancel() {
    this.cancelRegister.emit(false);
    this.router.navigateByUrl(ClientEndpoint.Home());
  }

  notFound() {
    this.cancelRegister.emit(false);
    this.router.navigateByUrl('/404');
  }
}
