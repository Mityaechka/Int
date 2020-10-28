export class SecondRound {
  constructor(public id: number, public questions: TruthQuestion[]) {}
}
export class TruthQuestion {
  constructor(
    public id: number,
    public value: string,
    public isTruth: boolean,
    public index: string
  ) {}
}
