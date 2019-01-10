import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AgmCoreModule } from '@agm/core';
import { ClarityModule } from '@clr/angular';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { MapComponent } from './components/map/map.component';
import { LocationMarkerComponent } from './components/location-marker/location-marker.component'

@NgModule({
  declarations: [
    AppComponent,
    EmployeeListComponent,
    MapComponent,
    LocationMarkerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ClarityModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCo7qa4U6kUL0BqwgoJSFfxMhFl3SBYyrc'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
