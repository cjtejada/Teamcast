import { Injectable } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { AuthDialogComponent } from '../nav/auth-dialog/auth-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  isLoggedIn: boolean = false;

  constructor(public dialog: MatDialog) { }

  launchAuthDialog(){
    const dialogRef = this.dialog.open(AuthDialogComponent, {height: '530px', width: '600px'});

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    })
  }

}
