import { environment } from './environment';

export class ApiEndpoint {
  static server: string = `${environment.API_PROTOCOL}://${environment.API_URL}:${environment.API_PORT}`;
  static authentication: string = `${this.server}/api/Authentication`;
  static employee: string = `${this.server}/api/Employee`;
}
