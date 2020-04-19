import { User } from './user';
import { TeamMember } from './teammember';

export interface Team {
  id: number;
  teamName: string;
  teamDescription: string;
  teamPhoto: number;
  createdDate: Date;
  user: User;
  teamMember?: TeamMember[];
}
