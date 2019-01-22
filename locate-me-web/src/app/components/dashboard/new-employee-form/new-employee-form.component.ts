import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ApiService } from '../../../services/api.service';
import { EmployeeListComponent } from '../../employee-list/employee-list.component';

@Component({
  selector: 'new-employee-form',
  templateUrl: './new-employee-form.component.html',
  styleUrls: ['./new-employee-form.component.scss']
})
export class NewEmployeeFormComponent {
  @Input() isOpen: boolean;
  @Output() isOpenChange = new EventEmitter<boolean>();
  employeeId: string;

  newEmployeeForm = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
  });
  
  constructor(
    private apiService: ApiService,
  ) { }

  closeModal() {
    this.isOpen = false;
    this.isOpenChange.emit(this.isOpen);
  }

  resetForm() {
    this.newEmployeeForm.reset();
  }

  submit() {
    let employee = <Employee>{};
    
    employee.FirstName = this.newEmployeeForm.value.firstName;
    employee.LastName = this.newEmployeeForm.value.lastName;
    console.log(employee.FirstName);
    console.log(employee.LastName);
    this.employeeId = this.apiService.CreateEmployee("86c06754-d473-4e77-9350-d61ba8cf190b", employee);
    console.log(employee.Id);
    this.closeModal();
  }
}
