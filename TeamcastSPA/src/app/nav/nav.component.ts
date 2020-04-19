import { Component, OnInit } from "@angular/core";
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { CreateEventDialogComponent } from './create-event-dialog/create-event-dialog.component';
import { AuthService } from '../services/auth.service';

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"],
})
export class NavComponent implements OnInit {

  searchValue: any = "events";
  searchBarHint: any;

  constructor(public dialog: MatDialog, private authService: AuthService) {}

  ngOnInit() {
    this.generateSearchHint();
  }

  generateSearchHint() {
    if (this.searchValue == "events") this.searchBarHint = "Search for a location...";
    else if (this.searchValue == "teams") this.searchBarHint = "Search for a team name...";
    else if (this.searchValue == "users") this.searchBarHint =  "Search a name or a username...";
  }

  createEventDialog(){
    const dialogRef = this.dialog.open(CreateEventDialogComponent, {height: '800px', width: '600px'});

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    })
  }

  IsDisabled(){
    if (this.searchValue != "events")
      return true;
  }

}
