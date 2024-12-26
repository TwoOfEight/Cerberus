import { Component } from '@angular/core';

@Component({
  selector: 'app-unauthorized-access',
  imports: [],
  template: `
    <div class="h-screen flex items-center justify-center background-main">
      <span class="text-5xl text-red-600 font-bold">UNAUTHORIZED ACCESS</span>
    </div>
  `,
})
export class UnauthorizedAccessComponent {

}
