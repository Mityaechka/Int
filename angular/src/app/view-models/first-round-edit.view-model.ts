export class FirstRoundEditViewModel {
  constructor(
    public questionCosts: IntellectualQuestionCostEditViewModel[],
    public questionsCategories: QuestionsCategoryEditViewModel[]
  ) {}
}
export class IntellectualQuestionCostEditViewModel {
  constructor(public value: string) {}
}
export class QuestionsCategoryEditViewModel {
  constructor(
    public name: string,
    public questions: IntellectualQuestionEditViewModel[]
  ) {}
}
export class IntellectualQuestionEditViewModel {
  constructor(
    public value: string,
    public answers: IntellectualAnswerEditViewModel[]
  ) {}
}
export class IntellectualAnswerEditViewModel {
  constructor( public value: string) {}
}
