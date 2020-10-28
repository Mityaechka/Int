import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../../../../services/auth.service';
import { LoadingService } from '../../../../services/loading.service';
import { DialogsService } from '../../../../services/dialogs.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-play-auth',
  templateUrl: './play-auth.component.html',
  styleUrls: ['./play-auth.component.css'],
})
export class PlayAuthComponent implements OnInit {
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
    // if (response.isSuccess) {
    //   this.router.navigate(['/live/select']);
    // } else {
    //   this.dialogs.pushAlert(response.errorMessage);
    // }
  }
}
