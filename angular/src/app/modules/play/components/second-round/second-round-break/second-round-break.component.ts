import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { GameProcessService } from '../../../../../services/game-process.service';

@Component({
  selector: 'app-second-round-break',
  templateUrl: './second-round-break.component.html',
  styleUrls: ['./second-round-break.component.css'],
})
export class SecondRoundBreakComponent implements OnInit {
  statistic: any;
  constructor(
    private gameProcessService: GameProcessService,
    private ChangeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.gameProcessService.getStatistic().then((x) => {
      this.statistic = x.data;
      this.ChangeDetectorRef.detectChanges();
    });
  }

  async startWaitingThirdRound() {
    await this.gameProcessService.startWaitingThirdRound();
  }
}
