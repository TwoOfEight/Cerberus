import { catchError, map, Observable, of, tap, throwError } from 'rxjs';

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { ApiEndpoint } from '../environments/api-endpoint';
import { AuthenticationResponse, LoginRequest, RegistrationRequest } from '../models/authentication';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private baseUrl: string = ApiEndpoint.authentication;
  private http = inject(HttpClient);
  private static readonly key = "JWT_KEY";

  get jwt(): string {
    return sessionStorage.getItem(AuthenticationService.key) ?? ''
  }

  private set jwt(value: string) {
    sessionStorage.setItem(AuthenticationService.key, value);
  }

  get isLoggedIn(): boolean {
    return !!this.jwt;
  }

  register(entity: RegistrationRequest): Observable<AuthenticationResponse> {
    return this.http
      .post<AuthenticationResponse>(`${this.baseUrl}/Register`, entity)
      .pipe(
        map((response) => {
          //this.cookieService.setCookies(response);
          return response;
        }),
        catchError(this.handleError)
      );
  }

  login(entity: LoginRequest): Observable<AuthenticationResponse> {
    return this.http
      .post<AuthenticationResponse>(`${this.baseUrl}/Login`, entity)
      .pipe(
        tap((response) => {
          this.jwt = response.jwtToken;
        }),
        map((response) => {
          //this.cookieService.setCookies(response);
          return response;
        }),
        catchError(this.handleError)
      );
  }

  logout(): void{
    sessionStorage.removeItem(AuthenticationService.key);
  }

  // getRole(id: string): Observable<String> {
  //   return this.http
  //     .get<String>(`${this.baseUrl}/getRole?id=${id}`)
  //     .pipe(catchError(this.handleError));
  // }

  // isAdmin(): Observable<boolean> {
  //   let id = this.cookieService.getUserId();
  //   if (id !== '') {
  //     return this.getRole(this.cookieService.getUserId()).pipe(
  //       map((role) => role === 'ADMIN'),
  //       catchError(this.handleError)
  //     );
  //   } else {
  //     return of(false);
  //   }
  // }

  // isModerator(): Observable<boolean> {
  //   let id = this.cookieService.getUserId();
  //   if (id !== '') {
  //     return this.getRole(this.cookieService.getUserId()).pipe(
  //       map((role) => role === 'MODERATOR'),
  //       catchError(this.handleError)
  //     );
  //   } else {
  //     return of(false);
  //   }
  // }

  // isAuthenticated() {
  //   return this.cookieService.checkToken();
  // }

  // getAuthToken() {
  //   return this.cookieService.getToken();
  // }

  // clearUser() {
  //   this.cookieService.removeCookies();
  // }

  /**
   * Handles errors that occur during HTTP requests.
   *
   * @param error - The error that occurred during the HTTP request.
   * @returns An Observable that emits an error.
   */
  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Unknown error occurred';
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
