import { ApiModel } from './../model/api.model';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { User } from '../entities/user.entity';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpService) {}
  async FindUsers(gameId: number, name: string): Promise<ApiModel<User[]>> {
    return await this.http.get<User[]>(
      `user/find?gameId=${gameId}&name=${name}`
    );
  }
}
