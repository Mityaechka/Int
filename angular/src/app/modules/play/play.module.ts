import { MatSidenavModule } from '@angular/material/sidenav';
import { CameraModule } from './../camera/camera.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CommonComponentsModule } from './../common-components/common-components.module';
import { PipesModule } from './../pipes/pipes.module';
import { FormatTimePipe } from '../pipes/formt-time.pipe';

import { MatFormFieldModule } from '@angular/material/form-field';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PlayRoutingModule } from './play-routing.module';
import { MainPlayComponent } from './components/main-play/main-play.component';
import { LobbyComponent } from './components/lobby/lobby.component';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { TimerComponent } from './components/timer/timer.component';
import { PleaseWaitComponent } from './components/please-wait/please-wait.component';
import { FirstRoundWaitingComponent } from './components/first-round/first-round-waiting/first-round-waiting.component';
import { FirstRoundPlayComponent } from './components/first-round/first-round-play/first-round-play.component';
import { MatDividerModule } from '@angular/material/divider';
import { SecondRoundWaitingComponent } from './components/second-round/second-round-waiting/second-round-waiting.component';
import { ThirdRoundWaitingComponent } from './components/third-round/third-round-waiting/third-round-waiting.component';
import { SecondRoundPlayComponent } from './components/second-round/second-round-play/second-round-play.component';
import { FirstRoundTableComponent } from './components/first-round/first-round-table/first-round-table.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { ChatComponent } from './components/chat/chat.component';
import { CameraComponentComponent } from './components/camera-component/camera-component.component';
import { AdComponent } from './components/ad/ad.component';
import { SecondRoundBreakComponent } from './components/second-round/second-round-break/second-round-break.component';
import { ThirdRoundPlayComponent } from './components/third-round/third-round-play/third-round-play.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { StatisticComponent } from './components/statistic/statistic.component';
import { MatTableModule } from '@angular/material/table';
import { CdkTableModule } from '@angular/cdk/table';
import { IvyCarouselModule } from 'angular-responsive-carousel';
import { PlayAuthComponent } from './components/play-auth/play-auth.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ThirdRoundBreakComponent } from './components/third-round/third-round-break/third-round-break.component';
@NgModule({
  declarations: [
    MainPlayComponent,
    LobbyComponent,
    TimerComponent,
    PleaseWaitComponent,
    FirstRoundWaitingComponent,
    FirstRoundPlayComponent,
    SecondRoundWaitingComponent,
    ThirdRoundWaitingComponent,
    SecondRoundPlayComponent,
    FirstRoundTableComponent,
    ChatComponent,
    CameraComponentComponent,
    AdComponent,
    SecondRoundBreakComponent,
    ThirdRoundPlayComponent,
    StatisticComponent,
    PlayAuthComponent,
    ThirdRoundBreakComponent,
  ],
  imports: [
    CameraModule,
    CommonModule,
    PlayRoutingModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    PipesModule,
    CommonComponentsModule,
    FlexLayoutModule,
    MatDividerModule,
    MatGridListModule,
    DragDropModule,
    MatTableModule,
    MatSidenavModule,
    CdkTableModule,
    MatListModule,
    IvyCarouselModule,
    MatTooltipModule,
  ],
  exports: [MainPlayComponent],
})
export class PlayModule {}
