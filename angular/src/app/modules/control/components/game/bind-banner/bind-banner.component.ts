import { logging } from 'protractor';
import { SponsorBanner } from './../../../../../entities/sponsor-banner.entity';
import { BannersService } from './../../../../../services/banners.service';
import { DialogsService } from './../../../../../services/dialogs.service';
import { DialogData } from './../../../../../model/dialog-data.mode';
import { LoadingService } from './../../../../../services/loading.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, OnInit, Inject, ChangeDetectorRef } from '@angular/core';
import { MatSelectionListChange } from '@angular/material/list';

@Component({
  selector: 'app-bind-banner',
  templateUrl: './bind-banner.component.html',
  styleUrls: ['./bind-banner.component.css'],
})
export class BindBannerComponent implements OnInit {
  banners: SponsorBanner[];
  constructor(
    @Inject(MAT_DIALOG_DATA) private gameId: number,
    private loading: LoadingService,
    private dialogs: DialogsService,
    private bannersService: BannersService,
    private detector: ChangeDetectorRef
  ) {}

  async ngOnInit() {
    this.loading.start();
    const response = await this.bannersService.getBanners();
    this.loading.stop();
    this.banners = response.data;
    this.detector.detectChanges();
  }
  async onSelectBanner(banner: MatSelectionListChange) {
    this.loading.start();
    const response = await this.bannersService.addToGame(
      banner.option.value.id,
      this.gameId
    );
    this.loading.stop();
    if (response.isSuccess) {
      this.dialogs.pop();
    } else {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
}
