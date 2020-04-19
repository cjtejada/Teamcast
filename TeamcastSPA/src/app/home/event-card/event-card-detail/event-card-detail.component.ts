import { Component, OnInit, Inject } from "@angular/core";
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from "@angular/material/dialog";
import { EventModel } from "src/app/models/eventmodel";
import { User } from "src/app/models/user";
import { Teams } from "./teams";

@Component({
  selector: "app-event-card-detail",
  templateUrl: "./event-card-detail.component.html",
  styleUrls: ["./event-card-detail.component.css"],
})
export class EventCardDetailComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<EventCardDetailComponent>,
    @Inject(MAT_DIALOG_DATA) public event: EventModel
  ) {}

  hasMembers: boolean;
  users: User[] = [];
  teamUsers: any[] = [];
  teamNames: string[] = [];
  teams: Teams[] = [];

  ngOnInit() {
    if (this.event.eventMember.length == 0)
      this.hasMembers = false;
    else
      this.getMembers();
  }

  getMembers() {
    let memberUser = 0,
      team = 0;

    this.event.eventMember.forEach((member) => {
      if (member.user !== null) {
        this.users[memberUser] = member.user;
        memberUser++;
      }

      if (member.user === null) {
        let teammate: any[] = [];
        let teamMemeber = 0;

        teammate[teamMemeber] = member.team.user;

        teamMemeber++;

        if (member.team.teamMember.length !== 0) {
          member.team.teamMember.forEach((teammem) => {
            teammate[teamMemeber] = teammem.user;
            teamMemeber++;
          });
        }

        this.teams[team] = {
          teamName: member.team.teamName,
          teammate: teammate,
        };

        team++;
      }
    });
  }

  closeEventDetail() {
    this.dialogRef.close();
  }
}
