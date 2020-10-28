import { User } from './user.entity';
export class Voucher {
  constructor(
    public id: number,
    public user: User,
    public code: string,
    public isActive: boolean
  ) {}
}
