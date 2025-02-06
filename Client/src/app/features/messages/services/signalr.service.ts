import {Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr";
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection: signalR.HubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${environment.backendUrl}/messagehub`)
    .configureLogging(signalR.LogLevel.Information)
    .build();


  startConnection() {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.error('Error while starting connection: ', err));
  }

  addMessageListener(methodName: string, callback: (...args: any[]) => void) {
    this.hubConnection.on(methodName, callback);
  }


  onReceiveMessage(callback: (message: string) => void) {
    this.hubConnection.on('ReceiveMessage', callback);
  }

  sendMessage(message: string) {
    return this.hubConnection.invoke('SendMessageToClients', message);
  }
}
