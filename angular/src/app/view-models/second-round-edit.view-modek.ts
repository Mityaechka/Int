export class SecondRoundEditViewModel {
  constructor(
    public questions: TruthQuestionEditViewModel[]
  ) { }
}
export class TruthQuestionEditViewModel {
  constructor(
    public value: string,
    public truthAnswer: string,
    public falseAnswer: string
  ) { }
}
