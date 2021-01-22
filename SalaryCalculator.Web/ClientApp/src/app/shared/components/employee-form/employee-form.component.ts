import { Component, Input, OnInit } from "@angular/core";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import { Employee } from "../../employee";
import { cloneDeep } from 'lodash';

@Component({
	selector: 'app-employee-form',
	templateUrl: './employee-form.component.html',
	styleUrls: ['./employee-form.component.css'],
})

export class EmployeeFormComponent implements OnInit {
	@Input()
	employee: Employee;

	@Input()
	serverErrorMessage: string;

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

	constructor(private fb: FormBuilder) {
			this.employeeTypes= ["Regular", "Contractual"];
		}

	ngOnInit(): void {
		if (this.employee != null) {
			this.form.patchValue(this.employee);
		} else {
		}
	}

	get valid(): boolean {
    this.form.markAllAsTouched();
    return this.form.valid;
  }

  get value(): Employee {
    const employee = cloneDeep(this.form.value as Employee);

    return employee;
  }
}