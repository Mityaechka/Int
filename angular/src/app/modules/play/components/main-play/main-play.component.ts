import { BannersService } from './../../../../services/banners.service';
import { SponsorBanner } from './../../../../entities/sponsor-banner.entity';
import { GameProcessService } from './../../../../services/game-process.service';
import {
  GameProcess,
  GameProcessState,
} from './../../../../entities/game-process.entity';
import { DialogsService } from './../../../../services/dialogs.service';
import { User } from './../../../../entities/user.entity';
import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
} from '@angular/core';
import { GameProcessHubService } from '../../../../services/game-process-hub.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AuthService } from '../../../../services/auth.service';
import { HubEvent } from '../../../../model/hub-event.model';
import { HubEventEnum } from '../../../../enums/hub-event.enum';
import { ChatMessage } from '../../../../entities/chat-message.entity';

@Component({
  selector: 'app-main-play',
  templateUrl: './main-play.component.html',
  styleUrls: ['./main-play.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainPlayComponent implements OnInit {
  user: User;
  gameProcess: GameProcess;
  chatMessages: ChatMessage[] = [];
  connectionId: string;

  streams: MediaStream[] = [];

  banners: SponsorBanner[];

  gameProcessId: number;
  constructor(
    private authService: AuthService,
    private gameProcessHub: GameProcessHubService,
    private route: ActivatedRoute,
    private router: Router,
    private dialogs: DialogsService,
    private gameProcessService: GameProcessService,
    private detector: ChangeDetectorRef,
    private bannersService: BannersService
  ) {}

  ngOnInit(): void {
    this.user = this.authService.user;
    this.authService.onUserCahngeSubscribe((user) => {
      this.user = user;
      if (user) {
        this.gameProcessHub.init();
        this.gameProcessHub.streamsEmitter.subscribe((streams) => {
          this.streams = streams;
          this.detector.detectChanges();
        });
        this.gameProcessHub.onConnect(() => {
          if (this.user) {
            this.gameProcessHub.invoke({
              name: 'ConnectUserToGame',
              data: this.gameProcessId,
            });
          } else {
            this.router.navigate(['/play/auth']);
          }
        });
        this.gameProcessHub.registerEvent<HubEvent<any>>({
          event: 'event',
          action: (event) => this.getEvent(event),
        });
      }
    });

    this.route.queryParamMap.subscribe((params) => {
      if (!params.get('id')) {
        return;
      }
      this.gameProcessId = Number.parseInt(params.get('id'), 10);

      this.router.navigate(['/play/auth']);
    });
  }
  async getEvent(event: HubEvent<any>) {
    console.log(event);
    switch (event.event) {
      case HubEventEnum.Error:
        this.dialogs.pushAlert(event.result.errorMessage, 'Произошла ошибка');
        break;
      case HubEventEnum.ConnectUserToGame:
        if (event.result.isSuccess) {
          const response = await this.gameProcessService.getGameProcess();
          if (response.isSuccess) {
            this.gameProcess = response.data;
            this.bannersService
              .gameBanners(this.gameProcess.gameId)
              .then((result) => {
                if (result.isSuccess) {
                  this.banners = result.data;
                }
                this.detector.detectChanges();
              });
            this.gameProcessService.joinToGame(undefined).then((result) => {
              this.gameProcessHub.invoke({ name: 'GetGameState' });
            });
            //this.router.navigate(['/play/join']);
          }
          const connectionIdResponse = await this.gameProcessService.getConnectionId();
          if (connectionIdResponse.isSuccess) {
            this.connectionId = connectionIdResponse.data;
          }
        }
        break;
      case HubEventEnum.GetGameState:
        const response = await this.gameProcessService.getGameProcess();
        if (response.isSuccess) {
          this.gameProcess = response.data;
        }
        switch (event.result.data as GameProcessState) {
          case GameProcessState.WaitingStart:
            this.router.navigate(['/play/timer']);
            break;
          case GameProcessState.WaitingFirstRound:
            this.router.navigate(['/play/rounds/first/wait']);
            break;
          case GameProcessState.FirstRound:
            this.router.navigate(['/play/rounds/first/play']);
            break;
          case GameProcessState.FirstRoundAd:
            this.router.navigate(['/play/rounds/first/ad']);
            break;
          case GameProcessState.WaitingSecondRound:
            this.router.navigate(['/play/rounds/second/wait']);
            break;
          case GameProcessState.SecondRound:
            this.router.navigate(['/play/rounds/second/play']);
            break;
          case GameProcessState.WaitingThirdRound:
            this.router.navigate(['/play/rounds/third/wait']);
            break;
          case GameProcessState.SecondRoundBreak:
            this.router.navigate(['/play/rounds/second/break']);
            break;
          case GameProcessState.ThirdRound:
            this.router.navigate(['/play/rounds/third/play']);
            break;
          case GameProcessState.ThirdRoundBreak:
            this.router.navigate(['/play/rounds/third/break']);
            break;
        }
        break;
      case HubEventEnum.SendChatMessage:
        this.chatMessages.push(event.result.data);
        break;
    }
  }
}
