import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LoadingService } from 'src/app/services/loading.service';
import { DialogsService } from 'src/app/services/dialogs.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    passwordConfirm: new FormControl('', [Validators.required]),
    gender: new FormControl(0, [Validators.required]),
    birthDate: new FormControl('', [Validators.required]),
  });
  constructor(
    private dialogs: DialogsService,
    private loading: LoadingService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  async register() {
    this.loading.start();
    const response = await this.authService.Register(this.form.getRawValue());
    this.loading.stop();
    if (response.isSuccess) {
      this.router.navigate(['/live/select']);
    } else {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
}
