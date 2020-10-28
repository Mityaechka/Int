import { DialogsService } from './../../../../../services/dialogs.service';
import { GameProcessService } from './../../../../../services/game-process.service';
import { HubEventEnum } from './../../../../../enums/hub-event.enum';
import { HubEvent } from './../../../../../model/hub-event.model';
import { GameProcessHubService } from './../../../../../services/game-process-hub.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FirstRoundQuestionViewModel } from '../../../../../view-models/first-round-question.view-model';

@Component({
  selector: 'app-first-round-play',
  templateUrl: './first-round-play.component.html',
  styleUrls: ['./first-round-play.component.css'],
})
export class FirstRoundPlayComponent implements OnInit {
  HubEventEnum = HubEventEnum;
  state: HubEventEnum;
  question: FirstRoundQuestionViewModel;
  time: Date;

  isAnswerSend = false;
  table: any;
  correctAnswer: { id: number; value: string };
  lastAnswer: { id: number; value: string };
  lastScore = 0;
  constructor(
    private gameProcessHubService: GameProcessHubService,
    private gameProcessService: GameProcessService,
    private detector: ChangeDetectorRef,
    private dialogs: DialogsService
  ) {}

  ngOnInit() {
    this.gameProcessHubService.registerEvent<HubEvent<any>>({
      event: 'event',
      action: (result) => this.onEvent(result),
    });
    this.gameProcessService.getFirstRoundTable().then((result) => {
      this.table = result.data;
      this.detector.markForCheck();
    });
  }
  onEvent(event: HubEvent<any>) {
    let ignore = false;
    switch (event.event) {
      case HubEventEnum.FirstRoundReceivingAnswers:
        this.isAnswerSend = false;
        this.question = event.result.data;
        this.question.time = new Date(this.question.time);
        break;
      case HubEventEnum.FirstRoundSelectQuestion:
        this.time = event.result.data.time;
        this.correctAnswer = event.result.data.correctAnswer;
        this.time = new Date(this.time);
        this.gameProcessService.getFirstRoundTable().then((result) => {
          this.table = result.data;
          this.detector.markForCheck();
        });
        if (!this.isAnswerSend) {
          this.lastAnswer = undefined;
          this.lastScore = 0;
        }
        break;
      default:
        ignore = true;
        break;
    }
    if (!ignore) {
      this.state = event.event;
      this.detector.markForCheck();
    }
  }
  async sendAnswer(answer: { id: number; value: string }) {
    const response = await this.gameProcessService.sendAnswerFirstRound(
      answer.id
    );
    if (response.isSuccess) {
      this.isAnswerSend = true;
      this.lastAnswer = answer;
      this.lastScore = response.data;
    } else {
      this.isAnswerSend = false;
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
}
