// src/app/services/signalr.service.ts
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({ providedIn: 'root' })
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5130/notify-admin') // Adjust based on your API's base URL
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR Connected!'))
      .catch(err => console.error('Error connecting SignalR', err));
  }

  addMessageListener(callback: (user: string, message: string) => void) {
    console.log("wait for messge");
    this.hubConnection.on('ReceiveExamNotification', callback);
    console.log("get message");
  }

  async SendMessage(): Promise<void>{
    try{
      await this.hubConnection.invoke('SendMessage');
    }
    catch (error){
      console.log("failed to send message")
    }
  }
}
