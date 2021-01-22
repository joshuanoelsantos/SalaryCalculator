import { Component, ViewChild } from "@angular/core";
import { MatDialogRef } from "@angular/material";
import { Employee } from "../../employee";
import { EmployeeFormComponent } from "../employee-form/employee-form.component";


@Component({
    selector: 'create-employee-dialog',
    templateUrl: 'create-employee-dialog.component.html',
  })
  export class CreateEmployeeDialog {

    @ViewChild(EmployeeFormComponent, {static: false})
    employeeForm: EmployeeFormComponent;

    employee: Employee;

    constructor(public dialogRef: MatDialogRef<CreateEmployeeDialog>){
      this.employee = new Employee();
    }

    onSave(): void {
      if (!this.employeeForm.valid) {
        return;
      }
      this.dialogRef.close();
    }
  
  }