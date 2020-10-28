import { Router } from '@angular/router';
import { DialogsService } from './../../../../services/dialogs.service';
import { LoadingService } from './../../../../services/loading.service';
import { GameService } from './../../../../services/game.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from './../../../../services/auth.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { User } from '../../../../entities/user.entity';

@Component({
  selector: 'app-select-game',
  templateUrl: './select-game.component.html',
  styleUrls: ['./select-game.component.css'],
})
export class SelectGameComponent implements OnInit {
  user: User;
  dates: Date[];
  dateForm = new FormGroup({
    date: new FormControl('', [Validators.required]),
    code: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(8),
    ]),
  });
  get dateControl() {
    return this.dateForm.controls.date as FormControl;
  }
  constructor(
    private authService: AuthService,
    private detector: ChangeDetectorRef,
    private gameService: GameService,
    private loading: LoadingService,
    private dialogs: DialogsService,
    private router:Router
  ) {}

  async ngOnInit() {
    this.loading.start();
    this.user = this.authService.user;
    if (!this.user) {
      const response = await this.authService.GetUser();
      if (response.isSuccess) {
        this.user = response.data;
      }
      const datesResponse = await this.gameService.getGamesDate();
      if (datesResponse.isSuccess) {
        this.dates = datesResponse.data;
      }
    }
    this.loading.stop();
    this.detector.markForCheck();
  }
  async joinToGame() {
    this.loading.start();
    const response = await this.gameService.joinWithVoucher(
      this.dateForm.getRawValue()
    );
    this.loading.stop();
    if (response.isSuccess) {
      this.router.navigate(['live/congratulation'])
    } else {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
}
