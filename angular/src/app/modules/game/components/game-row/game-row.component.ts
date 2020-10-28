import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-game-row',
  templateUrl: './game-row.component.html',
  styleUrls: ['./game-row.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GameRowComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
