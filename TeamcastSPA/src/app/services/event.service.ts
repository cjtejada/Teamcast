import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventModel } from '../models/eventmodel';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  baseUrl = "https://localhost:44393/api/Events/GetEvents"

  constructor(private http: HttpClient) { }

  getEvents(){
    return this.http.get<EventModel>(this.baseUrl);
  }

  postEvent(){
  }
}
