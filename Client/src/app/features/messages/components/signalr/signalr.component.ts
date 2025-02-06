import {Component, inject, OnInit, signal} from '@angular/core';
import {SignalrService} from '../../services/signalr.service';
import {MessageResponse} from '../../models/message-response';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'app-signalr',
  templateUrl: './signalr.component.html',
  styleUrls: ['./signalr.component.css'],
  standalone: true,
  imports: [
    DatePipe
  ]
})
export class SignalRComponent implements OnInit {
  private signalRService: SignalrService = inject(SignalrService);
  protected messages = signal<MessageResponse[]>([]);

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addMessageListener('ReceiveMessage', (message: MessageResponse) => {
      this.messages.update(msgs => [...msgs, message]);
    });
  }
}
