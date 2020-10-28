import { GameProcessService } from './../../../../services/game-process.service';
import { PlayerRole } from './../../../../entities/game.entity';
import { FormControl } from '@angular/forms';
import { User } from './../../../../entities/user.entity';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { GameProcessHubService } from '../../../../services/game-process-hub.service';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css'],
})
export class LobbyComponent implements OnInit {
  user: User;
  nickname = new FormControl('');
  constructor(
    private authService: AuthService,
    private gameProcessHub: GameProcessHubService,
    private gameProcessService: GameProcessService,
    private detector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.authService.GetUser().then((x) => {
      this.user = x.data;
      this.detector.detectChanges();
    });
    //this.user = this.authService.user;
  }
  async joinToGame() {
    const nickname = this.nickname.value;
    const response = await this.gameProcessService.joinToGame(nickname);
    this.gameProcessHub.invoke({ name: 'GetGameState' });
  }
}
