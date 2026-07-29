import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../../core/services/data.service';
import { RoomChartComponent } from '../../features/room-chart/room-chart';
import { SignalRService } from '../../core/services/signalr.service';
import { NotificationsComponent } from '../notification/notification.ts/notification';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RoomChartComponent, NotificationsComponent],
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

  constructor(
    private dataService: DataService,
    private signalR: SignalRService
  ) {}

  ngOnInit() {
    this.rooms.forEach(room => {
      this.dataService.getRoomFullData(room).subscribe(res => {
        console.log(res);
        this.data[room] = res;
      });
    });

    this.signalR.startConnection();
  }
}