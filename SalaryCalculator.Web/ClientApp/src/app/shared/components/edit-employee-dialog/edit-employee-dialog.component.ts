import { Component, Inject, ViewChild } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Employee } from "../../employee";
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

    constructor(public dialogRef: MatDialogRef<EditEmployeeDialog>,
        @Inject(MAT_DIALOG_DATA) private data: any
		) {
			this.employee = data.employee;
		}

    onSave(): void {
			if (!this.employeeForm.valid) {
				this.serverErrorMessage = "sfsdf 1";
				return;
			}
			this.serverErrorMessage = "sfsdf 2";
			// this.dialogRef.close();
		}
  
  }