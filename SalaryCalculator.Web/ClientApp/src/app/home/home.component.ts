import {Component, OnInit} from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { CreateEmployeeDialog } from '../shared/create-employee-dialog/create-employee-dialog.component';
import { Employee } from '../shared/employee';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  myControl = new FormControl();

  employee: Employee;

  employees: Employee[];

  constructor(private dialog: MatDialog){}

  ngOnInit() {

    this.employee = {
      id: "",
      name: "Joshua Santos",
      tin: "123-456-789",
      type: 1,
      birthDate: new Date(1994, 2, 8),
      salary: 70000,
    };

    this.employees = [{
      id: "",
      name: "Noel Santos",
      tin: "123-456-789",
      type: 0,
      birthDate: new Date(1994, 2, 8),
      salary: 70000,
    },{
      id: "",
      name: "Josh Castolome",
      tin: "123-456-789",
      type: 1,
      birthDate: new Date(1994, 2, 8),
      salary: 70000,
    },{
      id: "",
      name: "Joshua 2 Santos",
      tin: "123-456-789",
      type: 1,
      birthDate: new Date(1994, 2, 8),
      salary: 70000,
    }];
  }

  

  onEmployeeSelection(selectedEmployee: Employee, event, isOptionCreateNew: boolean) {
    if (event.isUserInput) {
      if(isOptionCreateNew === true) {

        const dialogRef = this.dialog.open(CreateEmployeeDialog, {
        });
    
        dialogRef.afterClosed().subscribe(result => {
        });

      } else {
        this.employee = selectedEmployee;
      }
      
    }
  }
}
