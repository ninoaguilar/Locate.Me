import { Component, Inject, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service'

@Component({
  selector: 'employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {
  title: string;
  selectedEmployee: Employee;
  employees: Employee[];

  constructor(
    private apiService: ApiService
  ) {
      this.title = "Employees";
  }

  ngOnInit() {
    this.employees = this.apiService.getAllEmployees("86c06754-d473-4e77-9350-d61ba8cf190b");
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
