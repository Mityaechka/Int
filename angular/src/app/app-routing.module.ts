import { CameraMainComponent } from './modules/camera/camera-main/camera-main.component';
import { MainControlComponent } from './modules/control/components/main-control/main-control.component';
import { MainPlayComponent } from './modules/play/components/main-play/main-play.component';
import { MainLiveComponentComponent } from './modules/live/components/main-live-component/main-live-component.component';

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { liveRoutes } from './modules/live/live-routing.module';
import { playRoutes } from './modules/play/play-routing.module';
import { controlRoutes } from './modules/control/control-routing.module';

const routes: Routes = [
  { path: 'camera', component: CameraMainComponent },
  {
    path: 'play',
    component: MainPlayComponent,
    children: playRoutes,
  },
  { path: 'live', component: MainLiveComponentComponent, children: liveRoutes },
  { path: 'control', component: MainControlComponent, children: controlRoutes },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
