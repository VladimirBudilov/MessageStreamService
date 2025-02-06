import {HttpClient, HttpParams} from '@angular/common/http';
import {Component, OnInit, signal} from '@angular/core';
import {MessageResponse} from '../../models/message-response';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'date-range',
  templateUrl: './date-range.component.html',
  imports: [
    DatePipe
  ],
  styleUrls: ['./date-range.component.css']
})
export class DateRangeComponent {
  recentMessages = signal<MessageResponse[]>([]);
  isLoading = signal(false);

  constructor(private http: HttpClient) {}

  fetchRecentMessages() {
    const now = new Date();
    const tenMinutesAgo = new Date(now.getTime() - 10 * 60000);

    const params = new HttpParams()
      .set('from', tenMinutesAgo.toISOString())
      .set('to', now.toISOString());

    this.isLoading.set(true);
    this.http.get<MessageResponse[]>('http://localhost:5000/api/messages', {params})
      .subscribe({
        next: (messages) => {
          this.recentMessages.set(messages);
          this.isLoading.set(false);
        },
        error: (error) => {
          console.error('Failed to fetch recent messages', error);
          this.isLoading.set(false);
        }
      });
  }
}
