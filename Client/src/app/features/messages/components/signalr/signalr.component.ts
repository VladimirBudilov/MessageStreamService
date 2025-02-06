import {Component, inject, OnInit, signal} from '@angular/core';
import {SignalrService} from '../../services/signalr.service';

@Component({
  selector: 'app-signalr',
  templateUrl: './signalr.component.html',
  styleUrls: ['./signalr.component.css'],
  standalone: true,
})
export class SignalRComponent implements OnInit {
  private signalRService: SignalrService = inject(SignalrService);
  protected messages = signal<string[]>([]);

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addMessageListener('ReceiveMessage', (message  : string) : void => {
      this.messages.update(msgs => [...msgs, message]);
    });
  }
}
