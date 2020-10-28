import { GameProcessService } from './../../../../../services/game-process.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-first-round-waiting',
  templateUrl: './first-round-waiting.component.html',
  styleUrls: ['./first-round-waiting.component.css'],
})
export class FirstRoundWaitingComponent implements OnInit {
  constructor(private gameProcessService: GameProcessService) {}

  ngOnInit(): void {}
  async startFirstRound() {
    await this.gameProcessService.startFirstRound();
  }
}
