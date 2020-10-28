import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoadingService } from 'src/app/services/loading.service';
import { LoginViewModel } from 'src/app/view-models/login.view-model';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private loading: LoadingService, private auth: AuthService, private router: Router) {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {

  }
  async login() {
    this.loading.start();
    console.log(this.loginForm);
    const response = await this.auth.Login(new LoginViewModel(this.loginForm.value.email,
      this.loginForm.value.password, true));

    this.loading.stop();
    if (response.isSuccess) {
      this.router.navigate(['/control/games']);
    }
  }
}
