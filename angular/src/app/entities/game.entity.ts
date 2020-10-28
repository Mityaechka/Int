import { User } from './user.entity';

export class Game {
  constructor(
    public id: number,
    public name: string,
    public gameState: GameState,
    public description: string,
    public gameUsers: GameUser[],
    public playersCount: number,
    public planeStartDate: Date,
    public maxPlayersCount: number
  ) {}
}

export enum GameState {
  Create,
  RecordingPlaysers,
  Playing,
  Finished,
}

export const GameStateDisplay: { [index: number]: string } = [
  'Создана',
  'Ожидание игроков',
  'В процессе',
  'Зывершено',
];
export enum PlayerRole {
  Player,
  Creator,
  Moderator,
}
export const PlayerRoleDisplay: { [index: number]: string } = [
  'Игрок',
  'Создатель',
  'Модератор',
];
export class GameUser {
  constructor(public user: User, public playerRole: PlayerRole) {}
}
