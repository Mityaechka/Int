import { GameProcessService } from './../../../../services/game-process.service';
import { GameProcess } from './../../../../entities/game-process.entity';
import { HubEvent } from './../../../../model/hub-event.model';
import { GameProcessHubService } from './../../../../services/game-process-hub.service';
import {
  Component,
  OnInit,
  Pipe,
  PipeTransform,
  ChangeDetectorRef,
} from '@angular/core';
import { HubEventEnum } from '../../../../enums/hub-event.enum';
import { Subscription, timer } from 'rxjs';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.css'],
})
export class TimerComponent implements OnInit {
  time: Date;
  gameProcess: GameProcess;
  constructor(
    private hub: GameProcessHubService,
    private detector: ChangeDetectorRef,
    private gameProcessService: GameProcessService
  ) {}

  ngOnInit(): void {
    this.gameProcess = this.gameProcessService.gameProcess;
    this.time = new Date(this.gameProcess.startTime);
  }
  async skipTimer() {
    const response = await this.gameProcessService.startWaitingFirstRound();
  }
}
