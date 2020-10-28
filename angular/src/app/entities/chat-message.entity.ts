import { User } from './user.entity';
export class ChatMessage {
  id: number;
  value: string;
  isSystem: boolean;
  nickname: string;
}
