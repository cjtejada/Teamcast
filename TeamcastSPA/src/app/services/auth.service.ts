import { Injectable } from '@angular/core';
import { AuthDialogComponent } from '../nav/auth-dialog/auth-dialog.component';
import {MatDialog} from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
import { Register } from '../models/register';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  user = new BehaviorSubject<User>(null);

  isLoggedIn: boolean = false;
  baseUrl = "https://localhost:5001/api/Auth/"
  currentUser: User;
  token: any;
  decodedToken: any;

  constructor(private http : HttpClient, public dialog: MatDialog) { }

  login(loginBody: any){
    return this.http.post(this.baseUrl + "Login", loginBody).pipe(
      map((response: any) => {
        const user = response[0];
        const token = response[1];
        if (user) {
          localStorage.setItem('token', token);
          localStorage.setItem('user', JSON.stringify(user.user));

          //????????????
          this.currentUser = user;
          this.token = token;
          //console.log(user + " | token : " + token)
        }
      })
    )
  }

  register(regBody: Register){
    return this.http.post(this.baseUrl + "Register", regBody).pipe(
      map((response: any) => {
        
        const user = response[0];
        const token = response[1];

        if (user) {
          localStorage.setItem('token', token);
          localStorage.setItem('user', JSON.stringify(user.user));
          //console.log(user + " | token : " + token)
        }

        return response;
      })
    );
  }

  launchAuthDialog(){
    const dialogRef = this.dialog.open(AuthDialogComponent, {height: '530px', width: '600px'});
  }

}
