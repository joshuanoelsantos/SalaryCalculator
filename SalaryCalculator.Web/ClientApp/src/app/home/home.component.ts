import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
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

  constructor(private employeeService: EmployeeService, private dialog: MatDialog){}

  ngOnInit() {

    this.employeeService
      .getAll()
      .subscribe((data) => {
        this.employees = data;

        if(this.employees && this.employees.length) {
          this.selectedEmployee = this.employees[0];
        } else {
          this.selectedEmployee = null;
        }

      });
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

  public editEmployee() {
    const dialogRef = this.dialog.open(EditEmployeeDialog, {
      data:{
        width: '600px',
        employee: this.employee
      }
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  private createNewEmployee() {
    const dialogRef = this.dialog.open(CreateEmployeeDialog, {
      width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      
      if(result) {
        this.refreshListWithSavedEmployee(result);
      }

    });
  }

  


  private refreshListWithSavedEmployee(employee: Employee) {
    this.employeeService
      .getAll()
      .subscribe((data) => {
        this.employees = data;

        if (this.employees && this.employees.length && employee) {
          let filteredList = this.employees.filter(x => x.id == employee.id);

          if(filteredList.length) {
            this.selectedEmployee = filteredList[0];
          }
          
        } else {
          this.selectedEmployee = null;
        }

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
