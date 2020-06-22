import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from './auth.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import {ReactiveFormsModule} from '@angular/forms';
import {MatCardModule, MatFormFieldModule} from '@angular/material';
import { ResetPasswordComponent } from './reset-password/reset-password.component';


@NgModule({
  declarations: [AuthComponent, LoginComponent, RegisterComponent, ResetPasswordComponent],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatFormFieldModule
  ],
  exports :[
    AuthComponent, 
    LoginComponent, 
    RegisterComponent,
    ResetPasswordComponent
  ]
})
export class AuthModule { }
