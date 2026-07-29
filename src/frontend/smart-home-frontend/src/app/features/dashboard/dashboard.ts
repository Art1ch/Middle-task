import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../../core/services/data.service';
import { RoomChartComponent } from '../../features/room-chart/room-chart';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RoomChartComponent],
  templateUrl: './dashboard.html'
})
export class DashboardComponent implements OnInit {
  rooms = [
    'Kitchen',
    'Bedroom',
    'Living Room',
    'Corridor',
    'Garage',
    'Office'
  ];

  data: any = {};

  constructor(private dataService: DataService) {}

  ngOnInit() {
    this.rooms.forEach(room => {
      this.dataService.getRoomFullData(room).subscribe(res => {
        console.log(res);
        this.data[room] = res;
      });
    });
  }
}