import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-main-live-component',
  templateUrl: './main-live-component.component.html',
  styleUrls: ['./main-live-component.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MainLiveComponentComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
