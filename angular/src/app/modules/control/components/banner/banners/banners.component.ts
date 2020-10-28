import { BannerCreateComponent } from './../banner-create/banner-create.component';
import { BannersService } from './../../../../../services/banners.service';
import { LoadingService } from './../../../../../services/loading.service';
import { DialogsService } from './../../../../../services/dialogs.service';
import { SponsorBanner } from './../../../../../entities/sponsor-banner.entity';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-banners',
  templateUrl: './banners.component.html',
  styleUrls: ['./banners.component.css'],
})
export class BannersComponent implements OnInit {
  banners: SponsorBanner[];
  constructor(
    private dialogs: DialogsService,
    private loading: LoadingService,
    private bannersService: BannersService,
    private detector: ChangeDetectorRef
  ) {}

  async ngOnInit() {
    this.loading.start();
    const response = await this.bannersService.getBanners();
    this.loading.stop();
    if (response.isSuccess) {
      this.banners = response.data;
    }
    this.detector.detectChanges();
  }
  createBanner() {
    this.dialogs.push({
      component: BannerCreateComponent,
      onInstance: (i) => {
        i.created.subscribe(async () => {
          this.loading.start();
          const response = await this.bannersService.getBanners();
          this.loading.stop();
          if (response.isSuccess) {
            this.banners = response.data;
          }
          this.detector.detectChanges();
        });
      },
    });
  }
}
