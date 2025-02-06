import {Routes} from '@angular/router';
import {MessageFormComponent} from './features/messages/components/message-form/message-form.component';
import {AppComponent} from './app.component';
import {SignalRComponent} from './features/messages/components/signalr/signalr.component';
import {DateRangeComponent} from './features/messages/components/date-range/date-range.component';

export const routes: Routes = [
  { path: 'message-form', component: MessageFormComponent },
  { path: 'signalr', component: SignalRComponent },
  { path: 'date-range', component: DateRangeComponent },
];

