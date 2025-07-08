import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs'
import {PaginatedResponse} from '../models/paginated-response.model';
import {Department} from '../models/department.model';
import {Employee} from '../models/employee.model';
import { EmployeeCreatePayload, EmployeeUpdatePayload } from '../models/employee-payload.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'https://localhost:7188/api';

  getEmployees(
    pageNumber: number,
    pageSize: number,
    filters: Record<string, string>,
    sort: { column: string | null, direction: 'asc' | 'desc' }
  ): Observable<PaginatedResponse<Employee[]>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    for (const key in filters) {
      if (filters[key]) {
        params = params.set(key, filters[key]);
      }
    }

    if (sort.column) {
      params = params.set('sortColumn', sort.column);
      params = params.set('sortDirection', sort.direction);
    }

    return this.http.get<PaginatedResponse<Employee[]>>(`${this.apiUrl}/Employee/GetAllEmployees`, {params});
  }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.apiUrl}/Department/GetAllDepartments`);
  }

  createEmployee(payload: EmployeeCreatePayload): Observable<Employee> {
    return this.http.post<Employee>(`${this.apiUrl}/Employee/CreateEmployee`, payload);
  }

  updateEmployee(payload: EmployeeUpdatePayload): Observable<Employee> {
    return this.http.patch<Employee>(`${this.apiUrl}/Employee/UpdateEmployee`, payload);
  }

  deleteEmployee(uuid: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Employee/DeleteEmployee?id=${uuid}`);
  }
}
