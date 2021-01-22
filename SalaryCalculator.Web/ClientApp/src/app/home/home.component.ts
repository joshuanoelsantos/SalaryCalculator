import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog, MatSnackBar } from '@angular/material';
import { CalculateSalaryDialog } from '../shared/components/calculate-salary-dialog/calculate-salary-dialog.component';
import { CreateEmployeeDialog } from '../shared/components/create-employee-dialog/create-employee-dialog.component';
import { EditEmployeeDialog } from '../shared/components/edit-employee-dialog/edit-employee-dialog.component';
import { Employee } from '../shared/employee';
import { EmployeeService } from '../shared/employee.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  myControl = new FormControl();

  employee: Employee;

  selectedEmployee: Employee;

  employees: Employee[];

  readonly employeeDialogFormWidth: "600px";

  constructor(
    private employeeService: EmployeeService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar){}

  ngOnInit() {

    this.refreshList();
  }

  onEmployeeSelection(selectedEmployee: Employee, event, isOptionCreateNew: boolean) {
    if (event.isUserInput) {
      if(isOptionCreateNew === true) {

        this.createNewEmployee();

      } else {
        this.employee = selectedEmployee;
      }
      
    }
  }

  public deleteEmployee() {
    this.employeeService
      .delete(this.employee.id)
      .subscribe(
        (data) => {
          this.snackBar.open("Successfully deleted.", null, {
            duration: 5000,
            panelClass: ['mat-toolbar', 'mat-primary'],
          });

          this.refreshList();
        },
        (error) => {

          let errorMessage: string;

          if(error && error.error && error.error.error) {
            errorMessage = error.error.error;
          } else {
            errorMessage = "Cannot save your data this time. Please try again later.";
          }

          this.snackBar.open(errorMessage, null, {
            duration: 5000,
            panelClass: ['mat-toolbar', 'mat-warn'],
          });
        });
  }

  public editEmployee() {
    const dialogRef = this.dialog.open(EditEmployeeDialog, {
      width: this.employeeDialogFormWidth,
      data:{
        employee: this.employee
      }
    });

    dialogRef.afterClosed().subscribe((result) => this.afterSaveFunction(result, true));
  }

  private createNewEmployee() {
    const dialogRef = this.dialog.open(CreateEmployeeDialog, {
      width: this.employeeDialogFormWidth
    });

    dialogRef.afterClosed().subscribe((result) => this.afterSaveFunction(result,false));
  }

  public calculateSalary() {
    this.dialog.open(CalculateSalaryDialog, {
      width: "400px",
      data:{
        employee: this.employee
      }
    });
  }

  private afterSaveFunction(result, isUpdate: boolean)
  {
    if(result) {
      this.refreshListWithSavedEmployee(result, isUpdate);
    }
  }

  private refreshList() {
    this.employeeService
      .getAll()
      .subscribe((data) => {
        this.employees = data;

        if (this.employees && this.employees.length) {
          this.selectedEmployee = this.employees[0];
          this.employee = this.employees[0];
        } else {
          this.selectedEmployee = null;
          this.employee = null;
        }

      });
  }

  private refreshListWithSavedEmployee(employee: Employee, isUpdate: boolean) {
    this.employeeService
      .getAll()
      .subscribe((data) => {
        this.employees = data;

        if (this.employees && this.employees.length && employee) {
          let filteredList = this.employees.filter(x => x.id == employee.id);

          if(filteredList.length) {
            this.selectedEmployee = filteredList[0];
            this.employee = filteredList[0];
          }
          
        } else {
          this.selectedEmployee = null;
          this.employee = null;
        }

        this.snackBar.open(`Successfully ${isUpdate ? 'updated' : 'created'}.`, null, {
          duration: 5000,
          panelClass: ['mat-toolbar', 'mat-primary'],
        });

      });
  }
    // this.employee = {
    //   id: "",
    //   name: "Joshua Santos",
    //   tin: "123-456-789",
    //   type: 1,
    //   birthDate: new Date(1994, 2, 8),
    //   salary: 70000,
    // };

    // this.employees = [{
    //   id: "",
    //   name: "Noel Santos",
    //   tin: "123-456-789",
    //   type: 0,
    //   birthDate: new Date(1994, 2, 8),
    //   salary: 70000,
    // },{
    //   id: "",
    //   name: "Josh Castolome",
    //   tin: "123-456-789",
    //   type: 1,
    //   birthDate: new Date(1994, 2, 8),
    //   salary: 70000,
    // },{
    //   id: "",
    //   name: "Joshua 2 Santos",
    //   tin: "123-456-789",
    //   type: 1,
    //   birthDate: new Date(1994, 2, 8),
    //   salary: 70000,
    // }];
}
