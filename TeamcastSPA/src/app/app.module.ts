import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NavComponent } from "./nav/nav.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatMenuModule } from "@angular/material/menu";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { MatSelectModule } from "@angular/material/select";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSliderModule } from "@angular/material/slider";
import { MatCardModule } from "@angular/material/card";
import { HomeComponent } from "./home/home.component";
import { RegisterComponent } from "./register/register.component";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MapCardComponent } from "./home/map-card/map-card.component";
import { EventCardComponent } from "./home/event-card/event-card.component";
import { ScrollingModule } from "@angular/cdk/scrolling";
import { MatDialogModule } from "@angular/material/dialog";
import { AuthDialogComponent } from "./nav/auth-dialog/auth-dialog.component";
import { MatDividerModule } from "@angular/material/divider";
import { EventCardDetailComponent } from "./home/event-card/event-card-detail/event-card-detail.component";
import { UserCompactComponent } from "./user/user-compact/user-compact.component";
import { UserDetailComponent } from "./user/user-detail/user-detail.component";
import { CreateEventDialogComponent } from "./nav/create-event-dialog/create-event-dialog.component";
import {MatDatepickerModule} from '@angular/material/datepicker';
import { EventService } from './services/event.service';
import { AuthService } from './services/auth.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MapCardComponent,
    EventCardComponent,
    AuthDialogComponent,
    EventCardDetailComponent,
    UserCompactComponent,
    UserDetailComponent,
    CreateEventDialogComponent
  ],
  entryComponents: [
    AuthDialogComponent,
    EventCardDetailComponent,
    CreateEventDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatMenuModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatFormFieldModule,
    MatSliderModule,
    MatCardModule,
    MatSidenavModule,
    ScrollingModule,
    MatDialogModule,
    MatDividerModule,
    HttpClientModule,
    MatDatepickerModule
  ],
  providers: [EventService, AuthService],
  bootstrap: [AppComponent],
})
export class AppModule {}
