import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import {ConfirmPasswordValidator} from './ConfirmPasswordValidator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  loading = false;
  errors: any = [];
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.initRegisterForm();
  }
  passwordsMatchValidator(control: FormControl): ValidationErrors {
    const password = control.root.get('password');
    return password && control.value !== password.value ? {
      passwordMatch: true
    }: null;
  }
  initRegisterForm() {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.compose([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100)
      ])
      ],
      email: ['', Validators.compose([
        Validators.required,
        Validators.email,
        Validators.minLength(3),
        // https://stackoverflow.com/questions/386294/what-is-the-maximum-length-of-a-valid-email-address
        Validators.maxLength(320)
      ]),
      ],
      username: ['', Validators.compose([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100)
      ]),
      ],
      password: ['', Validators.compose([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100)
      ])
      ],
      phoneNumber :[''],
      confirmPassword: ['', Validators.compose([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100)
      ])
      ],
      agree: [false, Validators.compose([Validators.required])]
    }, {
      validator: ConfirmPasswordValidator.MatchPassword
    });
  }
  get fullname(): any { return this.registerForm.get('fullname'); }
  get email(): any { return this.registerForm.get('email'); }
  get password(): any { return this.registerForm.get('password'); }
  get confirmPassword(): any { return this.registerForm.get('confirmPassword'); }
  submit() {
    console.log('inits');
  }

}
