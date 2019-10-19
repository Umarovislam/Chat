import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  private data: string;
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Debug)
      .withUrl('https://localhost:5001/chat',
        {
        skipNegotiation: true,
        //  accessTokenFactory: () => `Bearer ${JSON.parse(localStorage.getItem('currentUser')).Token}`,
        transport: signalR.HttpTransportType.WebSockets
      }).build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err.toString()));
  }
  public addTransferChartDataListener = () => {
    this.hubConnection.on('Receive', (data) => {
      this.data = data;
      console.log(data);
    });
  }
  public SendMessage(str: string) {
    return this.hubConnection.invoke('Send', str);
  }
  constructor() { }
}
