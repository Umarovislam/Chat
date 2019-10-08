import {Component, Inject, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {UserService} from '../../shared/user.service';
import {ApplicationUser} from '../../Entities/ApplicationUser';
import {tap} from 'rxjs/operators';
import {FormBuilder, Validators} from '@angular/forms';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  public myprofile = true;
  currentUser: ApplicationUser;
  EditForm  = {
    UserName: '',
    Email: '',
    Name: '',
    PhoneNumber: '',
    PictureUrl: undefined
  };
  me: UserInfo;
  changed = false;
  constructor(private router: Router, private userService: UserService, private forms: FormBuilder) { }

  ngOnInit() {
      this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
      this.userService.getUserById(this.currentUser.Id)
        .pipe(tap(_ => console.log(_)))
        .subscribe(
        data => {
          this.me = data
          this.EditForm.Name = data.Name;
          this.EditForm.UserName = data.UserName;
          this.EditForm.PictureUrl = data.PrictureUrl;
          this.EditForm.Email = data.Email;
          this.EditForm.PhoneNumber = data.PhoneNumber;
        }
      );
  }
  onSelectFile(event) {
    // tslint:disable-next-line:no-conditional-assignment
    if (event.target.files && (this.EditForm.PictureUrl = event.target.files[0])) {
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url
      // tslint:disable-next-line:no-shadowed-variable
      reader.onload = (event) => { // called once readAsDataURL is completed
        this.EditForm.PictureUrl = event.target.result;
      };
    }
  }

  public delete() {
    this.EditForm.PictureUrl = null;
  }
  Cancel() {
    this.router.navigate(['/home']);
  }
  SavaChanges() {
    console.log(typeof this.EditForm.PictureUrl)
    this.userService.updateUser(this.EditForm).subscribe(
      data => {
        this.router.navigate(['/home']);
      },
    );
  }
}
