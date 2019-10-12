import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';


@Injectable({
  providedIn: 'root'
})
const connection = new signalR.HubConnectionBuilder();
export class SignalRService {
  constructor() { }
}
