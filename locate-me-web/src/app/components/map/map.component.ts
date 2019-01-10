import { Component } from '@angular/core';

@Component({
  selector: 'map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent {
  title: string = 'My first AGM project';
  lat: number = 41.089876;
  lng: number = -111.974691;
}
