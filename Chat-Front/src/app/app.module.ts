import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {RouterModule} from '@angular/router';
import { AppComponent } from './app.component';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import {FormsModule} from '@angular/forms';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    FormsModule,
    RouterModule.forRoot([
        { path: '', redirectTo: '/', pathMatch: 'full' },
        { path: 'login', component: LoginComponent },
        { path: 'profile', component: ProfileComponent }])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
