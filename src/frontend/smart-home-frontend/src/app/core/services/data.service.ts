import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';
import { SensorDataItem, SensorTypeEnum } from '../../models/sensor-data.model';

@Injectable({ providedIn: 'root' })
export class DataService {
  private baseUrl = 'http://localhost:5020/SensorData/data';

  constructor(private http: HttpClient) {}

  getByRoomAndType(room: string, type: SensorTypeEnum): Observable<SensorDataItem[]> {
    const params = new HttpParams()
      .set('page', 1)
      .set('pageSize', 50)
      .set('placementName', room)
      .set('type', type);

    return this.http.get<SensorDataItem[]>(this.baseUrl, { params });
  }

  getRoomFullData(room: string) {
    return forkJoin({
      energy: this.getByRoomAndType(room, SensorTypeEnum.energy),
      air: this.getByRoomAndType(room, SensorTypeEnum.airQuality)
    });
  }
}