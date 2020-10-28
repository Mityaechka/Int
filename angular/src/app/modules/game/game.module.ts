import { MatTabsModule } from '@angular/material/tabs';
import { MatDialogModule } from '@angular/material/dialog';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameRowComponent } from './components/game-row/game-row.component';
import { MatListModule } from '@angular/material/list';
import { GameInfoComponent } from './components/game-info/game-info.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { GameJoinVoucherComponent } from './components/game-join-voucher/game-join-voucher.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { GameCreateComponent } from './components/game-create/game-create.component';
import { GameActiveComponent } from './components/game-active/game-active.component';
import { GameCreatorComponent } from './components/game-creator/game-creator.component';
import { GamePageComponent } from './components/game-page/game-page.component';
import { GameEditComponent } from './components/game-edit/game-edit.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { FirstRoundQuestionAnswerComponent } from './components/game-edit/first-round-components/first-round-question-answer/first-round-question-answer.component';
import { FirstRoundQuestionComponent } from './components/game-edit/first-round-question/first-round-question.component';

@NgModule({
  declarations: [
    GameRowComponent,
    GameInfoComponent,
    GameJoinVoucherComponent,
    GameCreateComponent,
    GameActiveComponent,
    GameCreatorComponent,
    GamePageComponent,
    GameEditComponent,
    FirstRoundQuestionAnswerComponent,
    FirstRoundQuestionComponent,
  ],
  imports: [
    CommonModule,
    MatListModule,
    MatCardModule,
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatTabsModule,
    MatExpansionModule,
    MatIconModule,
  ],
  exports: [
    GameRowComponent,
    GameCreateComponent,
    GameActiveComponent,
    GameCreatorComponent,
    GamePageComponent,
    GameEditComponent,
  ],
})
export class GameModule {}
