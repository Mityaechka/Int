import { Component, OnInit, ChangeDetectionStrategy, Input, Output, EventEmitter } from '@angular/core';
import { Game } from 'src/app/entities/game.entity';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GameListComponent implements OnInit {
  @Input() games: Game[];
  selected: Game;
  @Output() gameSelect: EventEmitter<Number> = new EventEmitter<Number>();
  constructor() { }

  ngOnInit(): void {
  }
  selectGame(event): void {
    this.selected = event.source.selectedOptions.selected[0]?.value;
    if (this.selected) {
      this.gameSelect.emit(this.selected.id);
    }
  }
}
