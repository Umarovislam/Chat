import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {UserService} from '../../shared/user.service';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styles: []
})
export class InfoComponent implements OnInit {
  user: UserInfo;
  editForm;
  constructor(private formBuilder: FormBuilder, private router: Router, private apiService: UserService) { }

  ngOnInit() {
    const userId = window.localStorage.getItem('editUserId');
    if (!userId) {
      alert('Invalid action.');
      this.router.navigate(['list-user']);
      return;
    }
  }
  /*editForm = this.formBuilder.group({
    id: [''],
    username: ['', Validators.required],
    email: ['', Validators.required],
    name: ['', Validators.required],
    phoneNumber: ['', Validators.required]
  });*/
}
