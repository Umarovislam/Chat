import { Injectable } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {BehaviorSubject, Observable} from 'rxjs';
import {ApplicationUser} from '../Entities/ApplicationUser';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly BaseURI = 'https://localhost:5001';
  private autwindow: Window;
  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<ApplicationUser>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }
  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FullName: ['', Validators.required],
    PhoneNumber: [''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(6)]],
      ConfirmPassword: ['']
    })
  });
  private currentUserSubject: BehaviorSubject<ApplicationUser>;
  public currentUser: Observable<ApplicationUser>;

  comparePasswords(fb: FormGroup) {
    const confirmPswrdCtrl = fb.get('ConfirmPassword');
    // passwordMismatch
    // confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
        confirmPswrdCtrl.setErrors(null);
    }
  }
  register() {
    // tslint:disable-next-line:prefer-const
    let body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password,
      PhoneNumber: this.formModel.value.PhoneNumber
    };
    return this.http.post(this.BaseURI + '/Account/Register', body);
  }

  logout() {
    this.http.get(this.BaseURI + '/Account/Logout');
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  login(formData) {
    return this.http.post<any>(this.BaseURI + '/Account/Login', formData).pipe(
      map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.Token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }

        return user;
      })
    );
  }
  public get currentUserValue(): ApplicationUser {
    return this.currentUserSubject.value;
  }

  getUserById(id: string): Observable<UserInfo> {
    return this.http.get<UserInfo>(this.BaseURI + '/UserProfile/GetUserProfile?' + 'id=' + id);
  }
  updateUser(a: any) {

    return this.http.put(this.BaseURI + '/UserProfile/UpdateUser', a);
  }
  getUserProfile() {
    return this.http.get('this.BaseURI' + '/UserProfile/GetUserProfile');
  }

}
