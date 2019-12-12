import {ChangeDetectorRef, Component, Inject, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {UserService} from '../../shared/user.service';
import {ApplicationUser} from '../../Entities/ApplicationUser';
import {tap} from 'rxjs/operators';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {any} from 'codelyzer/util/function';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  constructor(private router: Router, private userService: UserService, private fb: FormBuilder, private cd: ChangeDetectorRef) { }

  public  myprofile = true;
  currentUser: ApplicationUser;
  EditForm  = this.fb.group({
    UserName:  ['', Validators.required],
    Name: ['', Validators.required],
    Email: ['', Validators.required],
    PhoneNumber: ['', Validators.required],
    Avatar: any
  });
  me: UserInfo;
  changed = false;

  ngOnInit() {
      this.currentUser = JSON.parse( localStorage.getItem('currentUser'));
      this.getMyProfile();
  }
  public getMyProfile() {
    this.userService.getUserById(this.currentUser.Id)
      .pipe(tap(_ => console.log(_)))
      .subscribe(
        data => {
          this.me = data;
          this.EditForm.setValue(data);
        }
      );
  }
  public getUserProfile() {
    this.userService.getUserByUserName()
      .pipe(tap(_ => console.log(_)))
      .subscribe(
        data => {
          this.me = data;
        }
      );
  }
  onSelectFile(event) {
    const reader = new FileReader();
    // tslint:disable-next-line:no-conditional-assignment
    if (event.target.files && event.target.files.length) {
      const [PictureUrl] = event.target.files;
      reader.readAsDataURL(PictureUrl); // read file as data url
      // tslint:disable-next-line:no-shadowed-variable
      reader.onload = (event) => {
        this.me.Avatar = reader.result,
          this.EditForm.patchValue({
            PictureUrl: reader.result
          });
        this.cd.markForCheck();
      };
    }
  }

  public delete() {

  }
  Cancel() {
    this.router.navigate(['/home']);
  }
  SavaChanges() {
    console.log(typeof this.EditForm);
    this.userService.updateUser(this.EditForm).subscribe(
      data => {
        this.router.navigate(['/home']);
      },
    );
  }
}
