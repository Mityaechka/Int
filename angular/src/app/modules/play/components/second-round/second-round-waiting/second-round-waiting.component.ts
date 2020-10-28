import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { GameProcessService } from '../../../../../services/game-process.service';

@Component({
  selector: 'app-second-round-waiting',
  templateUrl: './second-round-waiting.component.html',
  styleUrls: ['./second-round-waiting.component.css'],
})
export class SecondRoundWaitingComponent implements OnInit {
  questionsCount = 0;
  constructor(
    private gameProcessService: GameProcessService,
    private detector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    // this.gameProcessService.getSecondRoundQuestionsCount().then((x) => {
    //   if (x.isSuccess) {
    //     this.questionsCount = x.data;
    //     this.detector.detectChanges();
    //   }
    // });
  }
  async startSecondRound() {
    await this.gameProcessService.startSecondRound();
  }
}
