import { User } from 'src/app/models/user';

export interface Teams{
  teamName: string;
  teammate: User[];
}
