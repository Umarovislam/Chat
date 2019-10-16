import { Component, OnInit } from '@angular/core';
import {UserService} from '../../shared/user.service';
import {FormBuilder, NgForm} from '@angular/forms';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {first, tap} from 'rxjs/operators';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  // tslint:disable-next-line:max-line-length
  constructor(private service: UserService, private router: Router, private toastr: ToastrService, private formBuilder: FormBuilder) {
  }


  submitted = false;
  returnUrl: string;
  formModel = {
  Email: '',
  Password: '',
};

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/home');
    }
  }
  onSubmit(form: NgForm) {
    this.submitted = true;
    this.service.login(this.formModel)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/home']);
        },
        error => {
          if (error.status === 400) {
            this.toastr.error('Incorrect username or password', 'Authentication failed');
          } else {
            console.log(error + error.status);
          }});


        /*      tap(_ => console.log(_))).subscribe(
              (res: any) => {
                localStorage.setItem('token', res);
                this.router.navigateByUrl('/home');
              },
              error => {
                if (error.status === 400) {
                  this.toastr.error('Incorrect username or password', 'Authentication failed');
                } else {
                  console.log(error + error.status);
                }
              }*/
  }
}
