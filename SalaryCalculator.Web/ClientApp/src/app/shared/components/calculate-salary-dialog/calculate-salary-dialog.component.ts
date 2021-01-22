import { Component, Inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Employee } from "../../employee";
import { EmployeeService } from "../../employee.service";


@Component({
    selector: 'calculate-salary-dialog',
    templateUrl: 'calculate-salary-dialog.component.html',
  })
  export class CalculateSalaryDialog {

    employee: Employee;
		computedSalary: number;
		serverErrorMessage: string;

		form: FormGroup = this.fb.group({
			input: [null, Validators.required],
		});

    constructor(
      private employeeService: EmployeeService,
      private dialogRef: MatDialogRef<CalculateSalaryDialog>,
      private fb: FormBuilder,
      @Inject(MAT_DIALOG_DATA) private data: any
      ) {
          this.employee = data.employee;
      }

			onCalculate(): void {
      
			this.form.markAllAsTouched();
			if(!this.form.valid)
				return;

      this.employeeService
        .calculateSalary(this.employee.id, this.form.get("input").value)
        .subscribe(
          (data) => {
						this.computedSalary = data;
          },
          (error) => {
            if(error && error.error && error.error.error) {
              this.serverErrorMessage = error.error.error;
            } else {
              this.serverErrorMessage = "Cannot save your data this time. Please try again later.";
            }
          });
		}
  
  }