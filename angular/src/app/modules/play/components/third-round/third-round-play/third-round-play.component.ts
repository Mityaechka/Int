import {
  Component,
  OnInit,
  ChangeDetectorRef,
  ElementRef,
  ViewChild,
} from '@angular/core';
import { HubEventEnum } from '../../../../../enums/hub-event.enum';
import { FirstRoundQuestionViewModel } from '../../../../../view-models/first-round-question.view-model';
import { GameProcessHubService } from '../../../../../services/game-process-hub.service';
import { GameProcessService } from '../../../../../services/game-process.service';
import { DialogsService } from '../../../../../services/dialogs.service';
import { HubEvent } from '../../../../../model/hub-event.model';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-third-round-play',
  templateUrl: './third-round-play.component.html',
  styleUrls: ['./third-round-play.component.css'],
})
export class ThirdRoundPlayComponent implements OnInit {
  ThirdRoundState = ThirdRoundState;
  thirdRoundState: ThirdRoundState;

  lastState: ThirdRoundState;

  lastChronology: any;
  lastMelody: any;
  lastAssociation: any;

  HubEventEnum = HubEventEnum;

  state: HubEventEnum;

  question: FirstRoundQuestionViewModel;

  time: Date;

  isAnswerSend = false;

  correctAnswer: { id: number; value: string };
  lastAnswer: { id: number; value: string };
  lastScore = 0;

  chronologyItems: any;

  melodyPath: any;
  melodyVariants: any;

  associationWord: string;
  associationVariants: any[];

  public reset: any[] = [{}];

  @ViewChild('melody') audioPlayerRef: ElementRef;
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
  }
  onEvent(event: HubEvent<any>) {
    let ignore = false;
    switch (event.event) {
      case HubEventEnum.ThirdRoundReceivingAnswers:
        this.isAnswerSend = false;
        this.thirdRoundState = event.result.data.state;
        this.time = event.result.data.time;
        this.time = new Date(this.time);

        switch (this.thirdRoundState) {
          case ThirdRoundState.Chronology:
            this.chronologyItems = event.result.data.chronologyItems;
            break;
          case ThirdRoundState.MelodyGuess:
            this.melodyPath = event.result.data.filePath;
            this.melodyVariants = event.result.data.melodyGuessVariants;
            break;
          case ThirdRoundState.Association:
            this.associationWord = event.result.data.word;
            this.associationVariants = event.result.data.associationVariants;
            break;
        }
        break;
      case HubEventEnum.ThirdRoundSelectQuestion:
        this.lastState = this.thirdRoundState;
        this.thirdRoundState = event.result.data.state;
        this.time = event.result.data.time;
        this.time = new Date(this.time);

        switch (this.thirdRoundState) {
          case ThirdRoundState.Chronology:
            this.lastChronology = event.result.data.chronology;
            break;
          case ThirdRoundState.MelodyGuess:
            this.lastMelody = event.result.data.melody;
            break;
          case ThirdRoundState.Association:
            this.lastAssociation = event.result.data.association;
            break;
        }

        if (!this.isAnswerSend) {
          this.lastAnswer = undefined;
          this.lastScore = -1;
        }
        break;
      default:
        ignore = true;
        break;
    }
    if (!ignore) {
      this.state = event.event;
      console.log(event.result.data);
    }
    this.reset[0] = {};
    this.detector.detectChanges();
  }

  dropChronologyItem(event: CdkDragDrop<string[]>) {
    moveItemInArray(
      this.chronologyItems,
      event.previousIndex,
      event.currentIndex
    );
  }
  async sendChronologyAnswer() {
    console.log();
    const response = await this.gameProcessService.sendAnswerThirdRoundChrology(
      this.chronologyItems.map((x) => x.id)
    );
    if (response.isSuccess) {
      this.isAnswerSend = true;
      //this.lastAnswer = answer;
      this.lastScore = response.data;
    } else {
      this.isAnswerSend = false;
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
  async sendMelodyAnswer(answer: any) {
    console.log();
    const response = await this.gameProcessService.sendAnswerThirdRoundMelody(
      answer.id
    );
    if (response.isSuccess) {
      this.isAnswerSend = true;
      //this.lastAnswer = answer;
      this.lastScore = response.data;
    } else {
      this.isAnswerSend = false;
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
  async sendAssociationAnswer(answer: any) {
    console.log();
    const response = await this.gameProcessService.sendAnswerThirdRoundAssociation(
      answer.id
    );
    if (response.isSuccess) {
      this.isAnswerSend = true;
      //this.lastAnswer = answer;
      this.lastScore = response.data;
    } else {
      this.isAnswerSend = false;
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
}

enum ThirdRoundState {
  Chronology = 0,
  MelodyGuess,
  Association,
}
