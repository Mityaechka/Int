import { LoadingService } from 'src/app/services/loading.service';
import { AuthService } from './../services/auth.service';
import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private loading: LoadingService
  ) {}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return new Observable((subscriber) => {
      this.loading.start();
      this.authService.GetUser().then((result) => {
        this.loading.stop();
        if (result.isSuccess) {
          subscriber.next(true);
        } else {
          this.router.navigate(['auth']);
          subscriber.next(false);
        }
        subscriber.complete();
      });
    });
  }
}
@Injectable({
  providedIn: 'root',
})
export class NotAuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return new Observable((subscriber) => {
      this.authService.GetUser().then((result) => {
        if (!result.isSuccess) {
          subscriber.next(true);
        } else {
          this.router.navigate(['main']);
          subscriber.next(false);
        }
        subscriber.complete();
      });
    });
  }
}
