import {
 Component,
 OnInit
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignalRService } from '../../../core/services/signalr.service';


@Component({
 selector:'app-notification',
 standalone:true,
 imports:[
   CommonModule
 ],
 templateUrl:'./notification.html'
})
export class NotificationsComponent
implements OnInit {


  messages:string[] = [];

  constructor(
    private signalR: SignalRService
  ){}

  ngOnInit(){

    this.signalR.messages$
    .subscribe(message => {
      this.messages.unshift(message);
    });
  }
}