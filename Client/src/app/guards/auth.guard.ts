import { inject, Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { ClientEndpoint } from '../environments/client-endpoint';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private userService = inject(AuthenticationService);
  private router = inject(Router);

  canActivate(): boolean {
    return true;
    // if (this.userService.isAuthenticated()) {
    //   return true;
    // } else {
    //   console.log('Thou shalt not pass!');
    //   this.router.navigate([ClientUri.getUnauthorizedAccessError()]);
    //   return false;
    // }
  }
}
