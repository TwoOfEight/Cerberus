import { Component } from '@angular/core';
import { NavbarComponent } from "./components/navbar.component"; 
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent],
  template: `
     <app-navbar></app-navbar>
     <router-outlet></router-outlet>
  `,
})
export class AppComponent {
  title = 'Client';
}
