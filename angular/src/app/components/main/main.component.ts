import { PlayerRole, Game, GameState } from './../../entities/game.entity';
import { DialogsService } from './../../services/dialogs.service';
import { LoadingService } from './../../services/loading.service';
import { GameService } from './../../services/game.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}
