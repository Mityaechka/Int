import { BannersComponent } from './components/banner/banners/banners.component';
import { GameCreateComponent } from './components/game/game-create/game-create.component';
import { GameEditComponent } from './components/game/game-edit/game-edit.component';
import { GamesComponent } from './components/game/games/games.component';

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { GamePageComponent } from './components/game/game-page/game-page.component';

export const controlRoutes: Routes = [
  { path: '', component: AuthComponent },
  { path: 'games', component: GamesComponent },
  { path: 'game', component: GamePageComponent },
  { path: 'game/edit', component: GameEditComponent },
  { path: 'game/create', component: GameCreateComponent },
  { path: 'banners', component: BannersComponent },
];

@NgModule({
  imports: [RouterModule.forChild(controlRoutes)],
  exports: [RouterModule],
})
export class ControlRoutingModule {}
