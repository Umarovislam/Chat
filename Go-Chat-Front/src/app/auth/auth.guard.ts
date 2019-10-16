import { Injectable } from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router} from '@angular/router';
import { Observable } from 'rxjs';
import {UserService} from '../shared/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router,private authenticationService: UserService) {

  }
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean  {
    const currentUser = this.authenticationService.currentUser;
    if (currentUser ) {
      return true;
    } else {
      this.router.navigate(['/user/login']);
      return false;
    }
  }

}
