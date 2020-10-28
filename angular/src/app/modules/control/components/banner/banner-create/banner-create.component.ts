import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  ChangeDetectorRef,
} from '@angular/core';
import { DialogsService } from '../../../../../services/dialogs.service';
import { LoadingService } from '../../../../../services/loading.service';
import { BannersService } from '../../../../../services/banners.service';

@Component({
  selector: 'app-banner-create',
  templateUrl: './banner-create.component.html',
  styleUrls: ['./banner-create.component.css'],
})
export class BannerCreateComponent implements OnInit {
  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    url: new FormControl('', [Validators.required]),
    file: new FormControl(undefined, [Validators.required]),
  });
  @Output() created = new EventEmitter();

  constructor(
    private dialogs: DialogsService,
    private loading: LoadingService,
    private bannersService: BannersService,
    private detector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {}
  async createBanner() {
    this.loading.start();
    const response = await this.bannersService.createBanner(
      this.form.getRawValue()
    );
    this.loading.stop();
    if (response.isSuccess) {
      this.created.emit();
      this.dialogs.pop();
    } else {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
}
