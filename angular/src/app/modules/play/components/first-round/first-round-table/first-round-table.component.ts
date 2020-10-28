import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-first-round-table',
  templateUrl: './first-round-table.component.html',
  styleUrls: ['./first-round-table.component.css'],
})
export class FirstRoundTableComponent implements OnInit {
  @Input() table: {
    categories: { name: string; id: number }[];
    cost: number[];
    firstRound: { categoryId: number; index: number }[];
  };
  constructor() {}

  ngOnInit(): void {}
  isAnswerTaken(categoryId: number, index: number) {
    return this.table.firstRound.find(
      (x) => x.categoryId === categoryId && x.index === index
    );
  }
}
