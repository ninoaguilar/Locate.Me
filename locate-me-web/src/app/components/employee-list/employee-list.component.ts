import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {
  title: string;
  selectedEmployee: Employee;
  employees: Employee[];

  constructor(/*http: HttpClient,
    @Inject('BASE_URL)') baseUrl: string*/) {
      this.title = "Employees";
      
      //var url = baseUrl + "api/companies/86c06754-d473-4e77-9350-d61ba8cf190b/employees"
/*
      http.get<Employee[]>(url).subscribe(result => {
        this.employees = result;
      }, error => console.error(error));
      */
  }

  ngOnInit() {
    this.employees = [
      { "Id": "1-1", "FirstName": "Nino", "LastName": "Aguilar" },
      { "Id": "1-2", "FirstName": "Nino", "LastName": "Aguilar" },
      { "Id": "1-3", "FirstName": "Nino", "LastName": "Aguilar" },
      { "Id": "1-4", "FirstName": "Nino", "LastName": "Aguilar" },
      { "Id": "1-5", "FirstName": "Nino", "LastName": "Aguilar" },
      { "Id": "1-6", "FirstName": "Nino", "LastName": "Aguilar" },
      { "Id": "1-7", "FirstName": "Nino", "LastName": "Aguilar" },
    ]
  }

  onSelect(employee: Employee) {
    this.selectedEmployee = employee;

    console.log("employee with Id " 
    + this.selectedEmployee.Id
    + "and name " 
    + this.selectedEmployee.FirstName + " " + this.selectedEmployee.LastName
    + " has been selected.");
  }
}
