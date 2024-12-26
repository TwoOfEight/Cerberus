import { Component, EventEmitter, inject, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { MaterialModule } from '../modules/material.module';
import { EmployeeService } from '../services/employee.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-employee',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule],
  template: `
    <form [formGroup]="form" (ngSubmit)="submitForm()">
      <mat-form-field>
        <mat-label>name</mat-label>
        <input matInput formControlName="name"/>
      </mat-form-field>
      <mat-form-field>
        <mat-label>rank</mat-label>
        <input matInput formControlName="rank"/>
      </mat-form-field>
      <button mat-raised-button>Save</button>
      <button mat-raised-button (click)="cancel()">Save</button>
    </form>
  `,
})
export class EmployeeAddComponent {
  @Output() onSaveSuccess: EventEmitter<void> = new EventEmitter<void>();

  public form!: FormGroup; 
  private _formBuilder = inject(FormBuilder);
  private _service = inject(EmployeeService);
  private _snackbar = inject(MatSnackBar);

  ngOnInit() {
    this.buildForm();
  }

  buildForm() {
    this.form = this._formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(7)]],
      rank: ['', [Validators.required, Validators.minLength(2)]],
    });
  }

  save(request: any) {
    console.log(`Sent the request: ${JSON.stringify(request, null, 2)}`)
    this._service.Create(request).subscribe({
      next: (response) => {
        console.log(`Got the response: ${JSON.stringify(response, null, 2)}`);
        this._snackbar.open('Object saved successfully.', 'Close', { duration: 3000, panelClass: ['success-snackbar'] });
        this.onSaveSuccess.emit();
      },
      error: (error) => {
        console.error(`Got the error: ${error}`)
        this._snackbar.open(`An error occured.`, 'Close', { duration: 3000, panelClass: ['error-snackbar'] });
      },
    });
  }

  submitForm() {
    if (this.form.valid) {
      const request = this.form.value;
      this.save(request);
    }
  }
  
  cancel() {
    this.form.reset();
  }
}

