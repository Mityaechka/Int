import { User } from './../entities/user.entity';
import { LoginViewModel } from './../view-models/login.view-model';
import { ApiModel } from './../model/api.model';
import { RegisterViewModel } from './../view-models/register.view-model';
import { HttpService } from './http.service';
import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _user: User;
  public get user(): User {
    return this._user;
  }

  constructor(private http: HttpService) {}

  private onUserCahnge = new EventEmitter<User>();

  public async Register(model: RegisterViewModel): Promise<ApiModel<any>> {
    const response = await this.http.post<any>(`auth/register`, model);
    this.onUserCahnge.emit((await this.GetUser()).data);
    return response;
  }
  public async Login(model: LoginViewModel): Promise<ApiModel<any>> {
    const response = await this.http.post<any>(`auth/login`, model);
    this.onUserCahnge.emit((await this.GetUser()).data);
    return response;
  }
  public async Logout(): Promise<ApiModel<any>> {
    const response = await this.http.get<any>(`auth/logout`);
    this.onUserCahnge.emit((await this.GetUser()).data);
    return response;
  }
  public async GetUser(): Promise<ApiModel<User>> {
    const response =  await this.http.get<User>(`account`);
    this._user = response.data;
    return response;
  }
  public async TriggerUser() {
    this.onUserCahnge.emit((await this.GetUser()).data);
  }
  public onUserCahngeSubscribe(event: (user: User) => void): void {
    this.onUserCahnge.subscribe(event);
  }
}
