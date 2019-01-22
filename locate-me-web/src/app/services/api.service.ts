import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

const API_URL = environment.apiUrl + "companies/";

@Injectable({
  providedIn: 'root'
})

export class ApiService {

  constructor(
    private http: HttpClient
  ) { }

  // API: GET /employees
  public getAllEmployees(companyId: string) {
    let employees: Employee[];

    var url = API_URL + companyId + "/employees"

    this.http.get<Employee[]>(url).subscribe(result => {
      employees = result;
    }, error => console.error(error));

    return employees;
  }

  // API: POST /employees
  public CreateEmployee(companyId: string, employee: Employee) {
    let employeeId: string;
    var url = API_URL + companyId + "/employees"

    this.http.post(url, {
      employee
    }).subscribe(result => {
      employeeId = result.toString();
      console.log(result);
    }, error => console.error(error));

    return employeeId;
  }

  // API: GET /employees/:id

  // API: PUT /employees/:id

  // API: DELETE /employees/:id
}
