import { Component, OnInit } from '@angular/core';
import {SignalRService} from '../../shared/services/signal-r.service';
import {HttpClient} from '@angular/common/http';
@Component({
  selector: 'app-chat-room',
  templateUrl: './chat-room.component.html',
  styleUrls: ['./chat-room.component.css']
})
export class ChatRoomComponent implements OnInit {


  constructor(public signalRService: SignalRService, private http: HttpClient) { }
  public froms = {
    str: ''
  };
  public data: any;
  messages: any[];
  ngOnInit() {
    this.signalRService.startConnection();
    this.data = this.signalRService.addTransferChartDataListener();

    // tslint:disable-next-line:no-unused-expression
  }
  private startHttpRequest = () => {
    // @ts-ignore
    this.signalRService.SendMessage(this.froms.str).catch(err => console.log(err.toString()));
  }
}
