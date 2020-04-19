import { Component, OnInit, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-auth-dialog',
  templateUrl: './auth-dialog.component.html',
  styleUrls: ['./auth-dialog.component.css']
})
export class AuthDialogComponent implements OnInit {
  hideLogin = true;
  hideReg = true;
  hideConfirm = true;

  constructor(
    public dialogRef: MatDialogRef<AuthDialogComponent>) {}

  ngOnInit() {
  }

  closeAuthDialog(){
    this.dialogRef.close();
  }

}
