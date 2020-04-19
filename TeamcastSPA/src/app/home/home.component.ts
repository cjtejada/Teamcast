import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { EventCardDetailComponent } from './event-card/event-card-detail/event-card-detail.component';
import { EventService } from '../services/event.service';
import { EventModel } from '../models/eventmodel';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  events: EventModel;

  constructor(public dialog: MatDialog, private eventService: EventService, private authService: AuthService) { }

  ngOnInit() {
    this.loadEvents();
  }

  loadEvents(){
    this.eventService.getEvents().subscribe((res: EventModel) => {
      this.events = res;
    },err => {
      alert(err.message);
    });
  }

  openEventDetailDialog(event: EventModel){
    const dialogRef = this.dialog.open(EventCardDetailComponent, { height: '800px', width: '1000px', data: event });
  }

  on

}
