import { Photo } from './Photo';

export interface User {
  id: number;
  username: string;
  knownAs: string;
  age: number;
  gender: string;
  createdDate: Date;
  lastActive: string;
  photoUrl: string;
  city: string;
  country: string;
  intresets?: string;
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
}
