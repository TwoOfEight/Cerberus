import { Component, inject } from '@angular/core';
import { MaterialModule } from '../modules/material.module';
import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-get-workers',
  imports: [MaterialModule],
  template: `
    <table mat-table [dataSource]="data" class="mat-elevation-z8">

    <ng-container matColumnDef="id">
      <th mat-header-cell *matHeaderCellDef> id </th>
      <td mat-cell *matCellDef="let element"> {{element.id}} </td>
    </ng-container>

    <ng-container matColumnDef="name">
      <th mat-header-cell *matHeaderCellDef> name </th>
      <td mat-cell *matCellDef="let element"> {{element.name}} </td>
    </ng-container>

    <ng-container matColumnDef="rank">
      <th mat-header-cell *matHeaderCellDef> rank </th>
      <td mat-cell *matCellDef="let element"> {{element.rank}} </td>
    </ng-container>

    <ng-container matColumnDef="delete">
      <th mat-header-cell *matHeaderCellDef> delete </th>
      <td mat-cell *matCellDef="let element"> 
        <button mat-raised-button (click)="delete(element.id)">X</button>
      </td>
    </ng-container>

    <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
    <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
    </table>
  `,
  styles:`
    .mat-column-name {
      width: 10vw;
    }
  `
})
export class EmployeeListComponent {
  displayedColumns: string[] = ['id', 'name', 'rank', 'delete'];
  data: Employee[] = [];

  private _service = inject(EmployeeService); 
  private _snackBar = inject(MatSnackBar)

  ngOnInit() {
    this.getAll();
  }

  getAll(): void {
    this._service.GetAll().subscribe({
      next: (response) => {
        this.data = response;
      },
      error: (error) => {
      // fallback
      },  
    })
  }

  delete(id: string): void {
    this._service.Delete(id).subscribe({
      next: (response) => {
      },
      error: (error) => {
        this._snackBar.open(`An error occured.`, 'Close', { duration: 3000, panelClass: ['error-snackbar'] });
      },  
    })
  }
}
