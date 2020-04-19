import { User } from './user';
import { EventMember } from './eventmember';

export interface EventModel {
  id: number;
  name: string;
  description: string;
  latitude: number;
  longitude: number;
  categoryType: number;
  startDateTime: Date;
  endDateTime: Date;
  maxMembers: number;
  compensationType: string;
  moneyCompensationAmount: number;
  otherCompensationDescription: string;
  createdDate: Date;
  eventOwner: User;
  eventMember?: EventMember[];
}
