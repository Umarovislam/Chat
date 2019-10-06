import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {UserService} from '../shared/user.service';
import {tap} from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {
  userDetails: any;

  constructor(private router: Router, private service: UserService) {
  }

  ngOnInit() {
  }

  OnLogout() {
    this.service.logout();
    this.router.navigate(['/user/login']);
  }

}
