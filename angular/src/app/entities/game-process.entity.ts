import { Game } from './game.entity';
export class GameProcess {
  constructor(
    public createDate: Date,
    public startTime: Date,
    public state: GameProcessState,
    public gameId: number
  ) {}
}

export enum GameProcessState {
  WaitingStart,
  WaitingFirstRound,
  FirstRound,
  FirstRoundAd,
  WaitingSecondRound,
  SecondRound,
  WaitingThirdRound,
  SecondRoundBreak,
  ThirdRound,
  ThirdRoundBreak,
}
