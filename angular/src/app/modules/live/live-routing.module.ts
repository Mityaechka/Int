import { CongratulationComponent } from './components/congratulation/congratulation.component';
import { SelectGameComponent } from './components/select-game/select-game.component';
import { AboutComponent } from './components/about/about.component';
import { ConnectComponent } from './components/connect/connect.component';
import { TitleComponent } from './components/title/title.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';

export const liveRoutes: Routes = [
  { path: '', component: TitleComponent },
  { path: 'connect', component: ConnectComponent },
  { path: 'about', component: AboutComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'select', component: SelectGameComponent },
  { path: 'congratulation', component: CongratulationComponent },
];

@NgModule({
  imports: [RouterModule.forChild(liveRoutes)],
  exports: [RouterModule],
})
export class LiveRoutingModule {}
