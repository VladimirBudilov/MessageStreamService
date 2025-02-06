import {Routes} from '@angular/router';
import {MessageFormComponent} from '../features/messages/message-form/message-form.component';
import {AppComponent} from './app.component';
import {SignalRComponent} from '../features/messages/signalr/signalr.component';
import {DateRangeComponent} from '../features/messages/date-range/date-range.component';

export const routes: Routes = [
  { path: 'message-form', component: MessageFormComponent },
  { path: 'signalr', component: SignalRComponent },
  { path: 'date-range', component: DateRangeComponent },
];

