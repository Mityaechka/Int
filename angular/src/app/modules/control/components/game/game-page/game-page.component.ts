import { BindBannerComponent } from './../bind-banner/bind-banner.component';
import { SponsorBanner } from './../../../../../entities/sponsor-banner.entity';
import { BannersService } from './../../../../../services/banners.service';
import { DialogsService } from './../../../../../services/dialogs.service';
import { LoadingService } from './../../../../../services/loading.service';
import { AuthService } from './../../../../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GameService } from './../../../../../services/game.service';
import { UserService } from './../../../../../services/user.service';
import { Voucher } from './../../../../../entities/voucher.entity';
import {
  GameUser,
  Game,
  PlayerRole,
  PlayerRoleDisplay,
  GameState,
  GameStateDisplay,
} from './../../../../../entities/game.entity';
import { User } from '../../../../../entities/user.entity';
import {
  Component,
  ChangeDetectionStrategy,
  OnInit,
  ChangeDetectorRef,
} from '@angular/core';
import { FindUserComponent } from '../../find-user/find-user.component';

@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GamePageComponent implements OnInit {
  gameId: number;
  game: Game;
  players: GameUser[];
  user: User;
  userRole: PlayerRole;
  PlayerRole = PlayerRole;
  playerRoleDisplay = PlayerRoleDisplay;
  GameState = GameState;
  GameStateDisplay = GameStateDisplay;
  isAdmin = false;
  hasModerator;
  vouchers: Voucher[];
  banners: SponsorBanner[];
  isInit = true;
  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private userService: UserService,
    private authService: AuthService,
    private loading: LoadingService,
    private dialogs: DialogsService,
    private detector: ChangeDetectorRef,
    private router: Router,
    private bannersService: BannersService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(async (params) => {
      console.log(params);
      this.isInit = true;
      this.gameId = params.id;
      this.loading.start();
      const userResponse = await this.authService.GetUser();
      if (!userResponse.isSuccess) {
        this.loading.stop();
        this.dialogs.pushAlert(userResponse.errorMessage);
      }
      const gameRepsponse = await this.gameService.GetGame(this.gameId);
      if (!gameRepsponse.isSuccess) {
        this.loading.stop();
        this.dialogs.pushAlert(gameRepsponse.errorMessage);
      }
      const playersReposne = await this.gameService.GetGamePlayers(this.gameId);
      if (!playersReposne.isSuccess) {
        this.loading.stop();
        this.dialogs.pushAlert(playersReposne.errorMessage);
      }
      const bannersResponse = await this.bannersService.gameBanners(
        this.gameId
      );
      if (bannersResponse.isSuccess) {
        this.banners = bannersResponse.data;
      }
      this.game = gameRepsponse.data;
      this.players = playersReposne.data;
      this.user = userResponse.data;

      this.userRole = this.players.find(
        (x) => x.user.id === this.user.id
      )?.playerRole;

      this.isAdmin =
        this.userRole === PlayerRole.Moderator ||
        this.userRole === PlayerRole.Creator;
      this.hasModerator = this.players.some(
        (x) => x.playerRole === PlayerRole.Moderator
      );

      if (this.isAdmin) {
        const vouchersResponse = await this.gameService.GetVouchers(
          this.gameId
        );

        if (vouchersResponse.isSuccess) {
          this.vouchers = vouchersResponse.data;
        }
      }
      this.isInit = false;
      this.detector.markForCheck();
      this.loading.stop();
    });
  }
  bindModeratorClick(): void {
    this.dialogs.push({
      component: FindUserComponent,
      data: {
        findEvent: (name) => this.onUsersFind(name),
        userSelectEvent: (userId) => this.onModeratorSelect(userId),
        component: this,
      },
    });
  }

  async onUsersFind(name: string): Promise<User[]> {
    const response = await this.userService.FindUsers(this.gameId, name);
    if (response.isSuccess) {
      return response.data;
    } else {
      this.dialogs.pushAlert(response.errorMessage);
      return null;
    }
  }
  async onModeratorSelect(userId: string): Promise<any> {
    this.dialogs.pop();
    this.loading.start();
    const response = await this.gameService.BindModeratorToGame(
      this.gameId,
      userId
    );

    if (!response.isSuccess) {
      this.loading.stop();
      this.dialogs.pushAlert(response.errorMessage);
    } else {
      const playersReposne = await this.gameService.GetGamePlayers(this.gameId);
      this.loading.stop();
      if (!playersReposne.isSuccess) {
        this.dialogs.pushAlert(playersReposne.errorMessage);
      }
      this.players = playersReposne.data;
      this.detector.detectChanges();
    }
  }
  async ChangeGameStateToRecordingClick(): Promise<any> {
    this.loading.start();
    const response = await this.gameService.ChangeGameStateToRecording(
      this.gameId
    );

    if (!response.isSuccess) {
      this.loading.stop();
      this.dialogs.pushAlert(response.errorMessage);
    } else {
      const gameRepsponse = await this.gameService.GetGame(this.gameId);
      this.loading.stop();
      if (!gameRepsponse.isSuccess) {
        this.dialogs.pushAlert(gameRepsponse.errorMessage);
      } else {
        this.game = gameRepsponse.data;
      }
    }
    this.detector.detectChanges();
  }
  async startGame(): Promise<any> {
    this.loading.start();
    const response = await this.gameService.startGame(this.gameId);
    this.loading.stop();
    if (!response.isSuccess) {
      this.dialogs.pushAlert(response.errorMessage);
    } else {
      this.router.navigate(['play'], { queryParams: { id: response.data } });
    }
    this.detector.detectChanges();
  }
  async CreateVoucher() {
    this.loading.start();
    const response = await this.gameService.CreateVoucher(this.gameId);
    if (!response.isSuccess) {
      this.loading.stop();
      this.dialogs.pushAlert(response.errorMessage);
    } else {
      const vouchersReposne = await this.gameService.GetVouchers(this.gameId);
      this.loading.stop();
      if (vouchersReposne.isSuccess) {
        this.vouchers = vouchersReposne.data;
      }
    }
    this.detector.detectChanges();
  }
  async AddBanner() {
    this.dialogs.push({ component: BindBannerComponent, data: this.gameId });
    this.detector.detectChanges();
  }
}
