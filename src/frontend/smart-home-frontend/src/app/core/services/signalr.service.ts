import { Injectable } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel
} from '@microsoft/signalr';

import { Subject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SignalRService {

    private baseUrl: string = 'http://localhost:5030/hubs/sensors';

    private connection!: HubConnection;
    private messageSubject = new Subject<string>();

    messages$ = this.messageSubject.asObservable();

    startConnection() {
        this.connection =
        new HubConnectionBuilder()
            .withUrl(
            this.baseUrl
            )
            .configureLogging(LogLevel.Information)
            .withAutomaticReconnect()
            .build();

        this.connection.on(
        'sensor-data',
        (message: string) => {
            console.log(
            'SignalR message:',
            message
            );
            this.messageSubject.next(message);
        }
        );

        this.connection
        .start()
        .then(() => {
            console.log(
            'SignalR connected'
            );
        })
        .catch(error => {
            console.error(
            'SignalR error',
            error
            );
        });

    }

    stopConnection(){
        if(this.connection){
        this.connection.stop();
        }
    }
}