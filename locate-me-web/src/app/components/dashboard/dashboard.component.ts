import { Component, OnChanges, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  isOpen = false;
  employees: Employee[];
  
  constructor(
    private  apiService:  ApiService
  ) { }

  ngOnInit() {
    this.employees = this.apiService.getAllEmployees("86c06754-d473-4e77-9350-d61ba8cf190b");
  }

  closeForm($event) {
    this.isOpen = $event;
  }

  openForm() {
    this.isOpen = true;
  }
}
