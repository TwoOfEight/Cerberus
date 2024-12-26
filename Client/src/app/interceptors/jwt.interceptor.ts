import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';

import { ApiEndpoint } from '../environments/api-endpoint';
import { AuthenticationService } from '../services/authentication.service';

export const jwtInterceptor: HttpInterceptorFn = (request, next) => {
  const authService = inject(AuthenticationService);

  const isLoggedIn = authService.isLoggedIn;
  const isSentToServer = request.url.startsWith(ApiEndpoint.server);

  let authRequest: HttpRequest<unknown> = request.clone();

  if (isLoggedIn && isSentToServer){
    authRequest = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${authService.jwt}`),
    });
	}
  // test

  return next(authRequest);
};