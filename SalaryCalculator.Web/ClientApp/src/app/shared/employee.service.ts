import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Employee } from "./employee";

@Injectable({
    providedIn: 'root',
	})
	
export class EmployeeService{
	baseUrl = 'api/employees';

	constructor(protected httpClient: HttpClient) {
	}

  getAll(): Observable<Employee[]> {

    return this.httpClient.get<Employee[]>(`${this.baseUrl}`, {});
  }
}