import { Component, OnInit, Input } from "@angular/core";
import { EventModel } from "src/app/models/eventmodel";
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: "app-event-card",
  templateUrl: "./event-card.component.html",
  styleUrls: ["./event-card.component.css"],
})
export class EventCardComponent implements OnInit {

  @Input() event: EventModel;
  isLocationProvided : Boolean = true;
  memberCount: number;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.countMembers();
  }

  countMembers() {

    let size = 0, i = 0;

    for (let em in this.event.eventMember) {
      size++;

      if (this.event.eventMember[i].team != null)
        for (let tm in this.event.eventMember[i].team["teamMember"])
          size++;

      i++;
    }
    this.memberCount = size;
  }

}
