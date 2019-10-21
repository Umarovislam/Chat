import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {UserService} from '../shared/user.service';
import {tap} from 'rxjs/operators';
import {UserProfileComponent} from '../user/user-profile/user-profile.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {
  userDetails: any;

  constructor(private router: Router, private service: UserService) {
  }
  foms = {
    str: ''
  }

  getUserProfile() {
    if (this.foms.str != null) {
    this.service.UserName = this.foms.str;
    this.router.navigate(['user/profile']);
    }
  }
  ngOnInit() {
  }

  OnLogout() {
    this.service.logout();
    this.router.navigate(['/user/login']);
  }

}
