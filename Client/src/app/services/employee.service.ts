import { catchError, Observable, tap, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiEndpoint } from '../environments/api-endpoint';
import { Employee, EmployeeCreate, EmployeePage } from '../models/employee';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private baseUrl: string = ApiEndpoint.employee;
  private http = inject(HttpClient);

  Create(entity: EmployeeCreate): Observable<Employee> {
    const url: string = `${this.baseUrl}/Create`;
    console.log(`Sending POST request to ${url} with data: ${JSON.stringify(entity, null, 2)}`);
    
    return this.http.post<Employee>(url, entity).pipe(
      tap((response) => {
        console.log(`Received response for create: ${JSON.stringify(response, null, 2)}`);
      }),
      catchError(this.handleError)
    );
  }

  GetAll(): Observable<Employee[]> {
    const url: string = `${this.baseUrl}/GetAll`;
    console.log(`Sending GET request to ${url}`);

    return this.http.get<Employee[]>(url).pipe(
      tap((response) => {
        console.log(`Received response: ${JSON.stringify(response, null, 2)}`);
      }),
      catchError(this.handleError)
    );
  }

  GetById(id: string): Observable<Employee> {
    const url: string = `${this.baseUrl}/GetById?id=${id}`;
    console.log(`Sending GET request to ${url}`);

    return this.http.get<Employee>(url).pipe(
      tap((response) => {
        console.log(`Received response for GetById: ${JSON.stringify(response, null, 2)}`);
      }),
      catchError(this.handleError)
    );
  }

  Update(entity: Employee): Observable<Employee> {
    const url: string = `${this.baseUrl}/Update`;
    console.log(`Sending PUT request to ${url} with data: ${JSON.stringify(entity, null, 2)}`);
    
    return this.http.put<Employee>(url, entity).pipe(
      tap((response) => {
        console.log(`Received response for update: ${JSON.stringify(response, null, 2)}`);
      }),
      catchError(this.handleError)
    );
  }

  Delete(id: string): Observable<boolean> {
    const url: string = `${this.baseUrl}/Delete?id=${id}`;
    console.log(`Sending DELETE request to ${url}`);

    return this.http.delete<boolean>(url).pipe(
      tap((response) => {
        console.log(`Received response for delete: ${JSON.stringify(response, null, 2)}`);
      }),
      catchError(this.handleError)
    );
  }

  GetPage(offset: number, pageSize: number): Observable<EmployeePage> {
    const url: string = `${this.baseUrl}/getPage?offset=${offset}&pageSize=${pageSize}`;
    console.log(`Sending GET request to ${url}`);

    return this.http.get<EmployeePage>(url).pipe(
      tap((response) => {
        console.log(`Received response for getPage: ${JSON.stringify(response, null, 2)}`);
      }),
      catchError(this.handleError)
    );
  }

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
