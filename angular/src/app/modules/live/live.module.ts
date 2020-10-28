import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TitleComponent } from './components/title/title.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LiveRoutingModule } from './live-routing.module';
import { MainLiveComponentComponent } from './components/main-live-component/main-live-component.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { AboutComponent } from './components/about/about.component';
import { ConnectComponent } from './components/connect/connect.component';
import { RegisterComponent } from './components/register/register.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SelectGameComponent } from './components/select-game/select-game.component';
import { CongratulationComponent } from './components/congratulation/congratulation.component';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';

@NgModule({
  declarations: [
    MainLiveComponentComponent,
    TitleComponent,
    AboutComponent,
    ConnectComponent,
    RegisterComponent,
    SelectGameComponent,
    CongratulationComponent,
  ],
  imports: [
    CommonModule,
    LiveRoutingModule,
    MatGridListModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    AngularSvgIconModule,
    HttpClientModule,
    FlexLayoutModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatCheckboxModule
  ],
  exports: [MainLiveComponentComponent],
})
export class LiveModule {}
