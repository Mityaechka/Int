import { ThirdRoundBreakComponent } from './components/third-round/third-round-break/third-round-break.component';
import { PlayAuthComponent } from './components/play-auth/play-auth.component';
import { ThirdRoundPlayComponent } from './components/third-round/third-round-play/third-round-play.component';
import { SecondRoundBreakComponent } from './components/second-round/second-round-break/second-round-break.component';
import { AdComponent } from './components/ad/ad.component';
import { SecondRoundPlayComponent } from './components/second-round/second-round-play/second-round-play.component';
import { ThirdRoundWaitingComponent } from './components/third-round/third-round-waiting/third-round-waiting.component';
import { SecondRoundWaitingComponent } from './components/second-round/second-round-waiting/second-round-waiting.component';
import { FirstRoundPlayComponent } from './components/first-round/first-round-play/first-round-play.component';
import { FirstRoundWaitingComponent } from './components/first-round/first-round-waiting/first-round-waiting.component';
import { PleaseWaitComponent } from './components/please-wait/please-wait.component';
import { TimerComponent } from './components/timer/timer.component';
import { MainPlayComponent } from './components/main-play/main-play.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LobbyComponent } from './components/lobby/lobby.component';

export const playRoutes: Routes = [
  { path: '', component: PleaseWaitComponent },
  { path: 'auth', component: PlayAuthComponent },
  { path: 'timer', component: TimerComponent },
  { path: 'join', component: LobbyComponent },
  { path: 'rounds/first/wait', component: FirstRoundWaitingComponent },
  { path: 'rounds/first/play', component: FirstRoundPlayComponent },
  { path: 'rounds/first/ad', component: AdComponent },
  { path: 'rounds/second/wait', component: SecondRoundWaitingComponent },
  { path: 'rounds/second/play', component: SecondRoundPlayComponent },
  { path: 'rounds/second/break', component: SecondRoundBreakComponent },
  { path: 'rounds/third/wait', component: ThirdRoundWaitingComponent },
  { path: 'rounds/third/play', component: ThirdRoundPlayComponent },
  { path: 'rounds/third/break', component: ThirdRoundBreakComponent },
];

@NgModule({
  imports: [RouterModule.forChild(playRoutes)],
  exports: [RouterModule],
})
export class PlayRoutingModule {}
