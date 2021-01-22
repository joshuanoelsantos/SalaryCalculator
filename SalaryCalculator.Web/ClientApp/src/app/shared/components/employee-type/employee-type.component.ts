import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-employee-type',
  templateUrl: './employee-type.component.html',
  styleUrls: ['./employee-type.component.css']
})
export class EmployeeTypeComponent {
  
  private _item: number;
  public display: string;

  // use getter setter to define the property
  get value(): any { 
    return this._item;
  }
  
  @Input()
  set value(val: any) {
    console.log('previous item = ', this._item);
    console.log('currently selected item=', val);
    this._item = val;

    if(this.value === 0) {
      this.display = "Regular";
    } else if(this.value === 1) {
      this.display = "Contractual";
    } else {
      this.display = "--";
    }
  }
}