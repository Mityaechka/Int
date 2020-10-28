import { GameProcess } from './../entities/game-process.entity';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GameProcessService {
  private _gameProcess: GameProcess;

  public get gameProcess(): GameProcess {
    return this._gameProcess;
  }

  constructor(private http: HttpService) {}
  async getGameProcess() {
    const response = await this.http.get<GameProcess>(`gameProcess`);
    this._gameProcess = response.data;
    return response;
  }
  async getConnectionId() {
    return await this.http.get<string>(`gameProcess/connectionId`);
  }
  async getStatistic() {
    return await this.http.get<any>(`gameProcess/statistic`);
  }
  async joinToGame(nickname: string) {
    return await this.http.get<any>(
      `gameProcess/join${nickname ? `?nickname=${nickname}` : ''}`
    );
  }
  async startWaitingFirstRound() {
    return await this.http.get<any>(`gameProcess/rounds/first/wait`);
  }
  async startFirstRound() {
    return await this.http.get<any>(`gameProcess/rounds/first/start`);
  }
  async sendAnswerFirstRound(id: number) {
    return await this.http.get<any>(`gameProcess/rounds/first/answer?id=${id}`);
  }
  async sendAnswerSecondRound(isTruth: boolean) {
    return await this.http.get<any>(
      `gameProcess/rounds/second/answer?isTruth=${isTruth}`
    );
  }
  async startSecondRound() {
    return await this.http.get<any>(`gameProcess/rounds/second/start`);
  }
  async getSecondRoundQuestionsCount() {
    return await this.http.get<number>(
      `gameProcess/rounds/second/questions-count`
    );
  }
  async getFirstRoundTable() {
    return await this.http.get(`gameProcess/rounds/first/table`);
  }
  async sendChatMessage(message: string) {
    return await this.http.post(
      `gameProcess/chat/messages?message=${message}`,
      null
    );
  }
  async startWaitingThirdRound() {
    return await this.http.get<any>(`gameProcess/rounds/third/wait`);
  }
  async startThirdRound() {
    return await this.http.get<any>(`gameProcess/rounds/third/start`);
  }
  async sendAnswerThirdRoundChrology(answers: number[]) {
    return await this.http.post<any>(
      `gameProcess/rounds/third/chronology/answer`,
      answers
    );
  }
  async sendAnswerThirdRoundMelody(answer: number) {
    return await this.http.get<any>(
      `gameProcess/rounds/third/chronology/answer?answer=${answer}`
    );
  }
  async sendAnswerThirdRoundAssociation(answer: number) {
    return await this.http.get<any>(
      `gameProcess/rounds/third/association/answer?answer=${answer}`
    );
  }
}
