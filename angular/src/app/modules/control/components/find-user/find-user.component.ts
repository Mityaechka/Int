import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Inject,
  ChangeDetectorRef,
} from '@angular/core';
import { User } from '../../../../entities/user.entity';
import { LoadingService } from '../../../../services/loading.service';

@Component({
  templateUrl: './find-user.component.html',
  styleUrls: ['./find-user.component.css'],
})
export class FindUserComponent implements OnInit {
  searchForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
  });

  findEvent: (name: string) => Promise<User[]>;
  userSelectEvent: (name: string) => Promise<User[]>;
  component: any;
  users: User[];
  constructor(
    @Inject(MAT_DIALOG_DATA)
    data: {
      findEvent: (name: string) => Promise<User[]>;
      userSelectEvent: (name: string) => Promise<User[]>;
      component: any;
    },
    private detector: ChangeDetectorRef,
    private loading: LoadingService
  ) {
    this.findEvent = data.findEvent;
    this.userSelectEvent = data.userSelectEvent;
    this.component = data.component;
  }

  ngOnInit(): void {}

  async onFindClick(): Promise<void> {
    this.loading.start();
    this.users = await this.findEvent(this.searchForm.controls.name.value);

    this.loading.stop();
    this.detector.markForCheck();
  }
  async onUserSelect(event): Promise<void> {
    const selected = event.source.selectedOptions.selected[0]?.value;
    if (selected) {
      await this.userSelectEvent(selected.id);
    }
  }
}
