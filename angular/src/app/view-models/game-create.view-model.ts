export class GameCreateViewModel {
  constructor(
    public name: string,
    public description: string,
    public planeStartDate: Date,
    public maxPlayersCount: number
  ) {}
}
