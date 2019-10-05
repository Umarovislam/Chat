import { Component, OnInit } from '@angular/core';
import {UserService} from '../../shared/user.service';
import {NgForm} from '@angular/forms';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {tap} from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  formModel = {
    Email: '',
    Password: '',
  };
  constructor(private service: UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/home');
    }
  }
  onSubmit(form: NgForm) {
    this.service.login(this.formModel).pipe(tap(_ => console.log(_))).subscribe(
      (res: string) => {
        localStorage.setItem('token', res);
        this.router.navigateByUrl('/home');
      },
      error => {
        if (error.status === 400) {
          this.toastr.error('Incorrect username or password', 'Authentication failed');
        } else {
          console.log(error + error.status);
        }
      }
    );
  }

}
