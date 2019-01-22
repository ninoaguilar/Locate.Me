import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { environment } from '../environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AgmCoreModule } from '@agm/core';
import { ClarityModule } from '@clr/angular';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule,
         ReactiveFormsModule } from '@angular/forms';

import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { MapComponent } from './components/map/map.component';
import { LocationMarkerComponent } from './components/location-marker/location-marker.component'
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NewEmployeeFormComponent } from './components/dashboard/new-employee-form/new-employee-form.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeeListComponent,
    MapComponent,
    LocationMarkerComponent,
    DashboardComponent,
    NewEmployeeFormComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ClarityModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AgmCoreModule.forRoot({
      apiKey: environment.apiKey
    }),
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
