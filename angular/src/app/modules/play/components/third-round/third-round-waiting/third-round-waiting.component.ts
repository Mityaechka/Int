import { GameProcessService } from './../../../../../services/game-process.service';
import { Component, OnInit } from '@angular/core';
import { threadId } from 'worker_threads';

@Component({
  selector: 'app-third-round-waiting',
  templateUrl: './third-round-waiting.component.html',
  styleUrls: ['./third-round-waiting.component.css'],
})
export class ThirdRoundWaitingComponent implements OnInit {
  constructor(private GameProcessService: GameProcessService) {}

  ngOnInit(): void {}
  start() {
    this.GameProcessService.startThirdRound();
  }
}
