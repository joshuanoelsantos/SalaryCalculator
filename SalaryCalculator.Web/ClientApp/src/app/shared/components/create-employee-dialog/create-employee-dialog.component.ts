import { Component, ViewChild } from "@angular/core";
import { MatDialogRef } from "@angular/material";
import { Employee } from "../../employee";
import { EmployeeService } from "../../employee.service";
import { EmployeeFormComponent } from "../employee-form/employee-form.component";


@Component({
    selector: 'create-employee-dialog',
    templateUrl: 'create-employee-dialog.component.html',
  })
  export class CreateEmployeeDialog {

    @ViewChild(EmployeeFormComponent, {static: false})
    employeeForm: EmployeeFormComponent;

    employee: Employee;
		serverErrorMessage: string;

    constructor(
      private employeeService: EmployeeService,
      private dialogRef: MatDialogRef<CreateEmployeeDialog>){

      this.employee = new Employee();
    }

    onSave(): void {
			if (!this.employeeForm.valid) {
				return;
			}
      
      this.employeeService
        .create(this.employeeForm.value)
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