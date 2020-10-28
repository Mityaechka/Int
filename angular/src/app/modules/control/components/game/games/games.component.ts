import { PlayerRole, Game } from './../../../../../entities/game.entity';
import { GameRowComponent } from './../../../../game/components/game-row/game-row.component';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { LoadingService } from '../../../../../services/loading.service';
import { GameService } from '../../../../../services/game.service';
import { DialogsService } from '../../../../../services/dialogs.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css'],
})
export class GamesComponent implements OnInit {
  games: Game[];
  constructor(
    private loading: LoadingService,
    private gameService: GameService,
    private dialogs: DialogsService,
    private detector: ChangeDetectorRef,
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    this.loading.start();
    this.games = (
      await this.gameService.GetGamesByRole(PlayerRole.Creator)
    ).data;
    this.detector.markForCheck();
    this.loading.stop();
  }
  async selectCreatorGame(id): Promise<void> {
    this.router.navigate(['/control/game'], { queryParams: { id } });
    // this.loading.start();
    // const gameResponse = await this.gameService.GetGameByRole(
    //   PlayerRole.Creator,
    //   id
    // );
    // this.loading.stop();

    // if (gameResponse.isSuccess) {
    //   this.dialogs.push({
    //     component: GameInfoComponent,
    //     data: gameResponse.data,
    //   });
    // } else {
    //   this.dialogs.pushAlert(gameResponse.errorMessage);
    // }
  }
}
