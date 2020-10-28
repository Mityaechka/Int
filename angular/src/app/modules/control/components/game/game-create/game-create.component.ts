import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { GameService } from '../../../../../services/game.service';
import { DialogsService } from '../../../../../services/dialogs.service';
import { LoadingService } from '../../../../../services/loading.service';

@Component({
  selector: 'app-game-create',
  templateUrl: './game-create.component.html',
  styleUrls: ['./game-create.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GameCreateComponent implements OnInit {
  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl(''),
    planeStartDate: new FormControl('', [Validators.required]),
  });
  minDate = new Date();
  constructor(
    private gameService: GameService,
    private dialogs: DialogsService,
    private loading: LoadingService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  async createGame() {
    this.loading.start();
    const response = await this.gameService.CreateGame(this.form.getRawValue());
    this.loading.stop();
    if (response.isSuccess) {
      this.router.navigate(['control/game'], {
        queryParams: { id: response.data },
      });
    } else {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
}
