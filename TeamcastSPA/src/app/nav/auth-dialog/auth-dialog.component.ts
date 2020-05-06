import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { AuthService } from 'src/app/services/auth.service';
import { Register } from '../../models/register'
import { NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-auth-dialog',
  templateUrl: './auth-dialog.component.html',
  styleUrls: ['./auth-dialog.component.css']
})
export class AuthDialogComponent implements OnInit {
  hideLogin = true;
  hideReg = true;
  hideConfirm = true;

  isLoading: boolean = false;

  @ViewChild('reg',{static: true}) regForm : NgForm;
  @ViewChild('login',{static: true}) loginForm : NgForm;

  constructor(
    public dialogRef: MatDialogRef<AuthDialogComponent>,
     private authService: AuthService,
     private snackBar: MatSnackBar) {}

  ngOnInit() {
  }

  onLogin(){

    this.isLoading = true;

    this.authService.login(
      {username: this.loginForm.value["username"]
      , password: this.loginForm.value["password"]})
      .subscribe(res => {

        this.authService.isLoggedIn = true;
        this.closeAuthDialog();

        this.snackBar.open("Successful login!", "Ok", {
          duration: 2000,
        });

      }, (err: HttpErrorResponse) => {

        this.snackBar.open(err.error["message"], "Ok", {
          duration: 3000,
        });

      });

      this.isLoading = false;  
  }

  onRegister(){

    this.isLoading = true;

    this.authService.register(
      {username: this.regForm.value["username"]
      , name: this.regForm.value["name"]
      , lastname: this.regForm.value["lastname"]
      , email: this.regForm.value["email"]
      , password: this.regForm.value["password"]})
      .subscribe(res => {

        this.authService.isLoggedIn = true;
        this.closeAuthDialog();
        this.snackBar.open("Register and login successful!", "Ok", {
          duration: 3000,
        });

      }, (err: HttpErrorResponse) => {

        this.snackBar.open(err.error["message"], "Ok", {
          duration: 3000,
        });

      });

      this.isLoading = false;
  }

  closeAuthDialog(){
    this.dialogRef.close();
  }

}
