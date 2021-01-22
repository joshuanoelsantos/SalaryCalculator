import { Component } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef } from "@angular/material/dialog";


@Component({
    selector: 'create-employee-dialog',
    templateUrl: 'create-employee-dialog.html',
    styleUrls: ['./create-employee-dialog.component.css'],
  })
  export class CreateEmployeeDialog {
  
    form: FormGroup = this.fb.group({
      id: [null],
      name: [null, [Validators.required, Validators.maxLength(500)]],
      birthDate: [null, [Validators.required]],
      tin: [null, [Validators.required, Validators.minLength(9), Validators.maxLength(17)]],
      type: [null, Validators.required],
      salary: [null, Validators.required],
    });

    get name() { return this.form.get('name'); }
    get tin() { return this.form.get('tin'); }
    get type() { return this.form.get('type'); }
    get salary() { return this.form.get('salary'); }

    employeeTypes: string[];

    constructor(
      private fb: FormBuilder,
      public dialogRef: MatDialogRef<CreateEmployeeDialog>) {

        this.employeeTypes= ["Regular","Contractual"];
      }
  
    onSave(): void {
      if (!this.form.valid) {
        this.validateAllFormFields(this.form);
        return;
      }
      this.dialogRef.close();
    }

    validateAllFormFields(formGroup: FormGroup) {         //{1}
      Object.keys(formGroup.controls).forEach(field => {  //{2}
        const control = formGroup.get(field);             //{3}
        if (control instanceof FormControl) {             //{4}
          control.markAsTouched({ onlySelf: true });
        } else if (control instanceof FormGroup) {        //{5}
          this.validateAllFormFields(control);            //{6}
        }
      });
    }
  
  }