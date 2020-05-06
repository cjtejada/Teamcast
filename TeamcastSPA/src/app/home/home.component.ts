import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EventCardDetailComponent } from './event-card/event-card-detail/event-card-detail.component';
import { EventService } from '../services/event.service';
import { EventModel } from '../models/eventmodel';
import { AuthService } from '../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  events: EventModel;
  isLoading: boolean = true;

  constructor(public dialog: MatDialog,
    private eventService: EventService,
    private authService: AuthService,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.loadEvents();
  }

  loadEvents() {

    this.isLoading = true;

    this.eventService.getEvents().subscribe((res: EventModel) => {
      this.events = res;
    }, (err: HttpErrorResponse)=> {

      alert(err.error);

      this.snackBar.open(err.error, "Ok", {
        duration: 2000,
      });

    });

    this.isLoading = false;
  }

  openEventDetailDialog(event: EventModel) {
    const dialogRef = this.dialog.open(EventCardDetailComponent, { height: '800px', width: '1000px', data: event });
  }

}
