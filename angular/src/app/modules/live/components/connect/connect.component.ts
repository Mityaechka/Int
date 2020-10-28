import { Router } from '@angular/router';
import { DialogsService } from './../../../../services/dialogs.service';
import { LoadingService } from './../../../../services/loading.service';
import { AuthService } from './../../../../services/auth.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-connect',
  templateUrl: './connect.component.html',
  styleUrls: ['./connect.component.css'],
})
export class ConnectComponent implements OnInit {
  authForm = new FormGroup({
    email: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });
  constructor(
    private authService: AuthService,
    private loading: LoadingService,
    private dialogs: DialogsService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  async auth() {
    this.loading.start();
    const response = await this.authService.Login(this.authForm.getRawValue());
    this.loading.stop();
    if (response.isSuccess) {
      this.router.navigate(['/live/select']);
    } else {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
}
