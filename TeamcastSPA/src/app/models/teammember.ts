import { User } from './user';

export interface TeamMember {
  role: string;
  dateJoned: Date;
  user: User;
}
