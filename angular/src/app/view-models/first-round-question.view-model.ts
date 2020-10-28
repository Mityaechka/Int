export class FirstRoundQuestionViewModel {
  constructor(
    public category: string,
    public question: string,
    public answers: { id: number; value: string }[],
    public time: Date,
    public cost: number
  ) {}
}
