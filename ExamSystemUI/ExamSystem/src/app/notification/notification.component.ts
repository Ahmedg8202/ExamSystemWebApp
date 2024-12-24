// src/app/components/notification.component.ts
import { Component, OnInit } from '@angular/core';
import { SignalRService } from './signalR.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-notification',
  standalone: true,
  templateUrl: './notification.component.html',
  styles: [],
  providers: [SignalRService],
  imports: [CommonModule],
})
export class NotificationComponent implements OnInit {
  messages: { user: string; message: string }[] = [];

  constructor(private signalRService: SignalRService) {}

  ngOnInit(): void {
    //debugger
    this.signalRService.startConnection();
    console.log("starts");
    this.signalRService.addMessageListener((user, message) => {
      console.log(user + " " + message);
      this.messages.push({ user, message });
    });
  }
}
