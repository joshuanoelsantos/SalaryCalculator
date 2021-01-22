import { Component, Inject, ViewChild } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Employee } from "../../employee";
import { EmployeeService } from "../../employee.service";
import { EmployeeFormComponent } from "../employee-form/employee-form.component";


@Component({
    selector: 'edit-employee-dialog',
    templateUrl: 'edit-employee-dialog.component.html',
  })
  export class EditEmployeeDialog {

    @ViewChild(EmployeeFormComponent, {static: false})
    employeeForm: EmployeeFormComponent;

		employee: Employee;
		serverErrorMessage: string;

    constructor(
      private employeeService: EmployeeService,
      private dialogRef: MatDialogRef<EditEmployeeDialog>,
        @Inject(MAT_DIALOG_DATA) private data: any
		) {
			this.employee = data.employee;
		}

    onSave(): void {
			if (!this.employeeForm.valid) {
				return;
			}
      
      this.employeeService
        .update(this.employeeForm.value)
        .subscribe(
          (data) => {
            this.dialogRef.close(data);
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