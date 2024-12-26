import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { ClientEndpoint } from './client-endpoint';

@Injectable({
  providedIn: 'root',
})
export class Navigator {
  private router = inject(Router);

  getHome(delay: number) {
    setTimeout(() => {
      this.router.navigate([ClientEndpoint.home]).then(() => {
        window.location.reload();
      });
    }, delay);
  }
}
