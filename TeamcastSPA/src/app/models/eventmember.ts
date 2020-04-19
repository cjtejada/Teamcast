import { User } from './user';
import { Team } from './team';

export interface EventMember {
  role: string;
  dateJoned: Date;
  user?: User;
  team?: Team;
}
