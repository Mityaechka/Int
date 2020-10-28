import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HubEventEnum } from '../../../../../enums/hub-event.enum';
import { FirstRoundQuestionViewModel } from '../../../../../view-models/first-round-question.view-model';
import { GameProcessHubService } from '../../../../../services/game-process-hub.service';
import { GameProcessService } from '../../../../../services/game-process.service';
import { DialogsService } from '../../../../../services/dialogs.service';
import { HubEvent } from '../../../../../model/hub-event.model';

@Component({
  selector: 'app-second-round-play',
  templateUrl: './second-round-play.component.html',
  styleUrls: ['./second-round-play.component.css'],
})
export class SecondRoundPlayComponent implements OnInit {
  HubEventEnum = HubEventEnum;
  state: HubEventEnum;
  question: string;
  time: Date;

  isAnswerSend = false;

  correctAnswer: { id: number; isTruth: boolean };
  lastAnswer: boolean;
  lastScore = 0;

  questionsCount = 0;
  usedQuestions = 0;
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
    this.gameProcessService.getSecondRoundQuestionsCount().then((x) => {
      if (x.isSuccess) {
        this.questionsCount = x.data;
      }
    });
  }
  onEvent(event: HubEvent<any>) {
    let ignore = false;
    switch (event.event) {
      case HubEventEnum.SecondRoundReceivingAnswers:
        this.isAnswerSend = false;
        this.question = event.result.data.question;
        this.time = new Date(event.result.data.time);
        break;
      case HubEventEnum.SecondRoundSelectQuestion:
        this.time = event.result.data.time;
        this.correctAnswer = event.result.data.correctAnswer;
        this.time = new Date(this.time);
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
  async sendAnswer(isTruth: boolean) {
    const response = await this.gameProcessService.sendAnswerSecondRound(
      isTruth
    );
    if (response.isSuccess) {
      this.isAnswerSend = true;
      this.lastAnswer = isTruth;
      this.lastScore = response.data;
    } else {
      this.isAnswerSend = false;
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
}
