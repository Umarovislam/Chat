import {Component, Inject, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {UserService} from '../../shared/user.service';
import {ApplicationUser} from '../../Entities/ApplicationUser';
import {tap} from 'rxjs/operators';
import {DOCUMENT} from '@angular/common';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  public myprofile = true;
  currentUser: ApplicationUser;
  me: UserInfo;
  changed = false;
  constructor(@Inject(DOCUMENT) document, private router: Router, private userService: UserService) { }

  ngOnInit() {
      this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
      this.userService.getUserById(this.currentUser.Id)
        .pipe(tap(_ => console.log(_)))
        .subscribe(
        data => {
          this.me = data;
        }
      );
  }
  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url
      // tslint:disable-next-line:no-shadowed-variable
      reader.onload = (event) => { // called once readAsDataURL is completed
        this.me.PrictureUrl = event.target.result;
      };
    }
  }

  public delete() {
    this.me.PrictureUrl = null;
  }
  Cancel() {
    this.router.navigate(['/home']);
  }
  SavaChanges() {
    this.userService.updateUser(this.me).subscribe(
      data => {
        this.router.navigate(['/home']);
      },
    );
  }
}
