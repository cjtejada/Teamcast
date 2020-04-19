import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-create-event-dialog',
  templateUrl: './create-event-dialog.component.html',
  styleUrls: ['./create-event-dialog.component.css']
})
export class CreateEventDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<CreateEventDialogComponent>) {}

  ngOnInit() {
  }

  closeEventCreateDialog(){
    this.dialogRef.close();
  }

}
