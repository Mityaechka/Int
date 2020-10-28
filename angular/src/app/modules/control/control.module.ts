import { FlexLayoutModule } from '@angular/flex-layout';
import { GameEditComponent } from './components/game/game-edit/game-edit.component';
import { GameCreateComponent } from './components/game/game-create/game-create.component';
import { FindUserComponent } from './components/find-user/find-user.component';

import { GamePageComponent } from './components/game/game-page/game-page.component';

import { GameListComponent } from './components/game/game-list/game-list.component';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ControlRoutingModule } from './control-routing.module';
import { MainControlComponent } from './components/main-control/main-control.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { PipesModule } from '../pipes/pipes.module';
import { CommonComponentsModule } from '../common-components/common-components.module';
import { AuthComponent } from './components/auth/auth.component';
import { GamesComponent } from './components/game/games/games.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { SponsorBannersComponent } from './components/sponsor-banner/sponsor-banners/sponsor-banners.component';
import { BannersComponent } from './components/banner/banners/banners.component';
import { BannerCreateComponent } from './components/banner/banner-create/banner-create.component';
import { NgxMatFileInputModule } from '@angular-material-components/file-input';
import { BindBannerComponent } from './components/game/bind-banner/bind-banner.component';

@NgModule({
  declarations: [
    MainControlComponent,
    AuthComponent,
    GamesComponent,
    GameListComponent,
    GamePageComponent,
    FindUserComponent,
    GameCreateComponent,
    GameEditComponent,
    SponsorBannersComponent,
    BannersComponent,
    BannerCreateComponent,
    BindBannerComponent,
  ],
  imports: [
    CommonModule,
    ControlRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatListModule,
    MatSidenavModule,
    MatExpansionModule,
    MatIconModule,
    MatToolbarModule,
    PipesModule,
    CommonComponentsModule,
    MatFormFieldModule,
    MatCheckboxModule,
    FlexLayoutModule,
    NgxMatFileInputModule,
  ],
  exports: [MainControlComponent],
})
export class ControlModule {}
