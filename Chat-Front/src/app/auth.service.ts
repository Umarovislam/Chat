import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  uri = 'https://localhost:44369/Account';
  token;
  userlog: LoginDto;
  constructor(private http: HttpClient, private route: Router) { }
  login(userlogin: LoginDto) {
    this.userlog = userlogin;
    this.http.post(this.uri + '/Login', userlogin)
      .subscribe((resp: any) => {
        this.route.navigate(['Profile']);
        localStorage.setItem('auth_token', resp.token);
      });
  }
  logout() {
    localStorage.removeItem('token');
  }

  public get logIn(): boolean {
    return (localStorage.getItem('token') !== null);
  }
}
