import { FirstRound } from './../entities/first-round.entity';
import { FirstRoundEditViewModel } from './../view-models/first-round-edit.view-model';
import { SecondRoundEditViewModel } from './../view-models/second-round-edit.view-modek';
import { Voucher } from './../entities/voucher.entity';
import { GameState, PlayerRole, GameUser } from './../entities/game.entity';
import { ApiModel } from './../model/api.model';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { Game } from '../entities/game.entity';
import { GameCreateViewModel } from '../view-models/game-create.view-model';
import { SecondRound } from '../entities/second-round.entity';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  constructor(private httpService: HttpService) {}
  async GetGamesByState(state: GameState): Promise<ApiModel<Game[]>> {
    return await this.httpService.get<Game[]>(`game/state/${state.toFixed()}`);
  }
  async GetGameByState(state: GameState, id): Promise<ApiModel<Game>> {
    return await this.httpService.get<Game>(`game/state/${state}/${id}`);
  }
  async GetGamesByRole(role: PlayerRole): Promise<ApiModel<Game[]>> {
    return await this.httpService.get<Game[]>(`game/role/${role.toFixed()}`);
  }
  async GetGameByRole(role: PlayerRole, id): Promise<ApiModel<Game>> {
    return await this.httpService.get<Game>(`game/role/${role}/${id}`);
  }
  async CreateGame(model: GameCreateViewModel): Promise<ApiModel<number>> {
    return await this.httpService.post<number>(`game/create`, model);
  }
  async GetGame(id): Promise<ApiModel<Game>> {
    return await this.httpService.get<Game>(`game/${id}`);
  }
  async GetGames(): Promise<ApiModel<Game[]>> {
    return await this.httpService.get<Game[]>(`game`);
  }
  async GetGamePlayers(id): Promise<ApiModel<GameUser[]>> {
    return await this.httpService.get<GameUser[]>(`game/${id}/players`);
  }
  async joinWithVoucher(data: { code: string; date: Date }) {
    return await this.httpService.get(
      `game/join/voucher?code=${data.code}&date=${data.date}`
    );
  }
  async BindModeratorToGame(
    gameId: number,
    moderatorId: string
  ): Promise<ApiModel<any>> {
    return await this.httpService.get<any>(
      `game/${gameId}/bindModeratorToGame?moderatorId=${moderatorId}`
    );
  }

  async ChangeGameStateToRecording(gameId: number): Promise<ApiModel<any>> {
    return await this.httpService.get<any>(
      `game/${gameId}/state/recordingPlayers`
    );
  }
  async startGame(gameId: number): Promise<ApiModel<any>> {
    return await this.httpService.get<any>(`game/${gameId}/start`);
  }
  async CreateVoucher(gameId: number): Promise<ApiModel<number>> {
    return await this.httpService.get<number>(`game/${gameId}/vouchers/add`);
  }
  async GetVouchers(gameId: number): Promise<ApiModel<Voucher[]>> {
    return await this.httpService.get<Voucher[]>(`game/${gameId}/vouchers`);
  }
  async GetFirstRound(gameId: number): Promise<ApiModel<FirstRound>> {
    return await this.httpService.get(`game/${gameId}/rounds/first`);
  }
  async GetSecondRound(gameId: number): Promise<ApiModel<SecondRound>> {
    return await this.httpService.get(`game/${gameId}/rounds/second`);
  }
  async EditThirdRound(gameId: number, model: any): Promise<ApiModel<any>> {
    return await this.httpService.postForm(
      `game/${gameId}/rounds/third/edit`,
      model
    );
  }
  async EditSecondRound(
    gameId: number,
    model: SecondRoundEditViewModel
  ): Promise<ApiModel<SecondRound>> {
    return await this.httpService.post(
      `game/${gameId}/rounds/second/edit`,
      model
    );
  }
  async EditFristRound(
    gameId: number,
    model: FirstRoundEditViewModel
  ): Promise<ApiModel<SecondRound>> {
    return await this.httpService.post(
      `game/${gameId}/rounds/first/edit`,
      model
    );
  }
  async saveGame(
    gameId: number,
    model: {
      name: string;
      description: string;
      maxPlayersCount: number;
      planeStartDate: Date;
    }
  ): Promise<ApiModel<SecondRound>> {
    return await this.httpService.post(`game/${gameId}/edit`, model);
  }
  async getGamesDate() {
    return await this.httpService.get<Date[]>(`game/dates`);
  }
}
