import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private fb: FormBuilder,private router: Router) { }
  loginForm: FormGroup;
  
  ngOnInit() {
    this.initFrom();
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/home');
    }
  }
  initFrom(){
    this.loginForm = this.fb.group({
      userName:['',Validators.compose([
        Validators.required
      ])],
      password: ['',Validators.compose([
        Validators.required,
        Validators.min(6)
      ])]
    })
  }
  onSubmit(form: any){
    console.log("login")
  }

}
