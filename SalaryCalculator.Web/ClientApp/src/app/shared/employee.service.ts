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

  create(employee: Employee): Observable<Employee> {
    return this.httpClient.post<Employee>(`${this.baseUrl}`, employee);
  }

  update(employee: Employee): Observable<Employee> {
    return this.httpClient.put<Employee>(`${this.baseUrl}/${employee.id}`, employee);
  }

  delete(id: string): Observable<Employee> {
    return this.httpClient.delete<Employee>(`${this.baseUrl}/${id}`);
  }

  calculateSalary(id: string, input: number): Observable<number> {
    return this.httpClient.post<number>(`${this.baseUrl}/${id}/calculate`, input);
  }
}