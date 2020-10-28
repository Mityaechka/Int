export class FirstRound {
  constructor(
    public id: number,
    public questionCosts: IntellectualQuestionCost[],
    public questionsCategories: QuestionsCategory[]
  ) {}
}
export class IntellectualQuestionCost {
  constructor(public id: number, public index: number, public value: string) {}
}
export class QuestionsCategory {
  constructor(
    public id: number,
    public name: string,
    public questions: IntellectualQuestion[]
  ) {}
}
export class IntellectualQuestion {
  constructor(
    public id: number,
    public value: string,
    public index: string,
    public answers: IntellectualAnswer[]
  ) {}
}
export class IntellectualAnswer {
  constructor(public id: number, public value: string) {}
}
