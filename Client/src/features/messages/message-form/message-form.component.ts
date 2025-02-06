import { Component } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-message-form',
  standalone: true,
  templateUrl: './message-form.component.html',
  styleUrls: ['./message-form.component.css'],
  imports: [
    ReactiveFormsModule,
    NgIf
  ]
})
export class MessageFormComponent {
  form: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.form = this.fb.group({
      id: [null, [Validators.required, Validators.min(1)]],
      text: ['', [Validators.required, Validators.maxLength(128)]],
    });
  }

  submitForm() {
    if (this.form.valid) {
      const message = this.form.value;
      this.http.post('http://localhost:5000/api/messages', message).subscribe({
        next: () => {
          alert('Message sent successfully!');
          this.form.reset();
        },
        error: (err) => {
          alert('Error sending message.');
          console.error(err);
        },
      });
    }
  }
}
