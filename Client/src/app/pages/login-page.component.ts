import { Component } from '@angular/core';
import { LoginComponent } from '../components/login.component';

@Component({
  selector: 'app-login-page',
  imports: [LoginComponent],
  template: `
    <div class="h-screen flex justify-center items-center background-main">
      <app-login></app-login>
    </div>
  `,
})
export class LoginPageComponent {}
