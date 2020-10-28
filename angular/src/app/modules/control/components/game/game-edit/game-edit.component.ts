import { SecondRound } from './../../../../../entities/second-round.entity';
import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { Game, PlayerRole } from '../../../../../entities/game.entity';
import { FirstRound } from '../../../../../entities/first-round.entity';
import {
  FormGroup,
  FormArray,
  FormControl,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { GameService } from '../../../../../services/game.service';
import { LoadingService } from '../../../../../services/loading.service';
import { DialogsService } from '../../../../../services/dialogs.service';
import {
  SecondRoundEditViewModel,
  TruthQuestionEditViewModel,
} from '../../../../../view-models/second-round-edit.view-modek';

@Component({
  selector: 'app-game-edit',
  templateUrl: './game-edit.component.html',
  styleUrls: ['./game-edit.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GameEditComponent implements OnInit {
  step = -1;
  gameId: number;
  game: Game;
  secondRound: SecondRound;
  firstRound: FirstRound;

  form: FormGroup = new FormGroup({
    game: new FormGroup({
      name: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      planeStartDate: new FormControl('', [Validators.required]),
      maxPlayersCount: new FormControl('', [Validators.required]),
    }),
    secondRound: new FormArray([]),
    firstRound: new FormGroup({
      questionsCategories: new FormArray([]),
    }),
    thirdRound: new FormGroup({
      chronologies: new FormArray([]),
      melodyGuesses: new FormArray([]),
      associations: new FormArray([]),
    }),
  });

  get gameForm(): FormGroup {
    return this.form.controls.game as FormGroup;
  }
  get secondRoundForm(): FormArray {
    return this.form.controls.secondRound as FormArray;
  }
  get firstRoundForm(): FormGroup {
    return this.form.controls.firstRound as FormGroup;
  }

  get firstRoundQuestionsCategories(): FormArray {
    return this.firstRoundForm.controls.questionsCategories as FormArray;
  }
  get thirdRound() {
    return this.form.controls.thirdRound as FormGroup;
  }
  get chronologies() {
    return this.thirdRound.controls.chronologies as FormArray;
  }
  get melodyGuesses() {
    return this.thirdRound.controls.melodyGuesses as FormArray;
  }
  get associations() {
    return this.thirdRound.controls.associations as FormArray;
  }
  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private loading: LoadingService,
    private dialogs: DialogsService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(async (params) => {
      this.gameId = params.id;
      const gameResponse = await this.gameService.GetGameByRole(
        PlayerRole.Creator,
        this.gameId
      );
      const secondRoundResponse = await this.gameService.GetSecondRound(
        this.gameId
      );
      if (secondRoundResponse.data) {
        this.fillSecondRoundForm(secondRoundResponse.data);
      }

      this.game = gameResponse.data;
      this.secondRound = secondRoundResponse.data;

      const firstRoundResponse = await this.gameService.GetFirstRound(
        this.gameId
      );
      if (firstRoundResponse.isSuccess) {
        if (firstRoundResponse.data == null) {
          this.fillFirstRoundFormEmpty();
        } else {
          this.fillFirstRoundForm(firstRoundResponse.data);
        }
      }
      this.form.controls.game.patchValue({
        name: this.game.name,
        description: this.game.description,
        planeStartDate: this.game.planeStartDate,
        maxPlayersCount: this.game.maxPlayersCount,
      });
    });
  }

  pushSecondRoundQuestion(): void {
    this.secondRoundForm.push(
      new FormGroup({
        value: new FormControl('', [Validators.required]),
        isTruth: new FormControl(false),
      })
    );
  }
  removeSecondRoundQuestion(index): void {
    this.secondRoundForm.removeAt(index);
  }
  saveAll() {
    this.saveSecondRound();
    this.saveFirstRound();
    this.saveThirdRound();
    this.saveGame();
    this.dialogs.pushAlert('Данные успешно сохранены');
  }
  async saveThirdRound(): Promise<void> {
    const c = this.secondRoundForm.getRawValue();
    const response = await this.gameService.EditThirdRound(
      this.gameId,
      this.thirdRound.getRawValue()
    );
    if (!response.isSuccess) {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
  async saveSecondRound(): Promise<void> {
    const c = this.secondRoundForm.getRawValue();
    const response = await this.gameService.EditSecondRound(this.gameId, {
      questions: this.secondRoundForm.getRawValue(),
    });
    if (!response.isSuccess) {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
  async saveFirstRound(): Promise<void> {
    const response = await this.gameService.EditFristRound(
      this.gameId,
      this.firstRoundForm.getRawValue()
    );
    if (!response.isSuccess) {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }

  async saveGame(): Promise<void> {
    const response = await this.gameService.saveGame(
      this.gameId,
      this.gameForm.getRawValue()
    );
    if (!response.isSuccess) {
      this.dialogs.pushAlert(response.errorMessage);
    }
  }
  fillFirstRoundFormEmpty() {
    for (let i = 1; i <= 4; i++) {
      this.firstRoundQuestionsCategories.push(
        new FormGroup({
          name: new FormControl(`Категория ${i}`),
          questions: new FormArray(
            [1, 2, 3, 4].map(
              (x) =>
                new FormGroup({
                  value: new FormControl(`Вопрос ${x}`),
                  answers: new FormArray(
                    [1, 2, 3, 4].map((u) => new FormControl(`Ответ ${u}`))
                  ),
                })
            )
          ),
        })
      );
    }
  }
  fillFirstRoundForm(round: FirstRound) {
    for (const category of round.questionsCategories) {
      this.firstRoundQuestionsCategories.push(
        new FormGroup({
          name: new FormControl(category.name),
          questions: new FormArray(
            category.questions.map(
              (x) =>
                new FormGroup({
                  value: new FormControl(x.value),
                  answers: new FormArray(
                    x.answers.map((u) => new FormControl(u.value))
                  ),
                })
            )
          ),
        })
      );
    }
  }
  fillSecondRoundForm(secondRound: SecondRound) {
    for (const question of secondRound.questions) {
      this.secondRoundForm.push(
        new FormGroup({
          value: new FormControl(question.value, [Validators.required]),
          isTruth: new FormControl(question.isTruth),
        })
      );
    }
  }
  addChronologie() {
    this.chronologies.push(
      new FormGroup({
        chronologyItems: new FormArray([]),
      })
    );
  }
  removeChronologie(i: number) {
    this.chronologies.removeAt(i);
    this.chronologies.patchValue([]);
  }
  addChronologieItem(control: AbstractControl, i: number) {
    const array = control as FormGroup;
    (array.controls.chronologyItems as FormArray).push(
      new FormGroup({
        file: new FormControl(undefined, [Validators.required]),
        value: new FormControl('', [Validators.required]),
      })
    );
  }
  removeChronologieItem(control: AbstractControl, i: number) {
    const array = control as FormGroup;
    (array.controls.chronologyItems as FormArray).removeAt(i);
    array.patchValue([]);
  }

  addMelody() {
    this.melodyGuesses.push(
      new FormGroup({
        file: new FormControl(undefined, [Validators.required]),
        melodyGuessVariants: new FormArray([
          new FormControl('', [Validators.required]),
          new FormControl('', [Validators.required]),
          new FormControl('', [Validators.required]),
          new FormControl('', [Validators.required]),
        ]),
      })
    );
  }
  removeMelody(i: number) {
    this.melodyGuesses.removeAt(i);
    this.melodyGuesses.patchValue([]);
  }

  addAssociation() {
    this.associations.push(
      new FormGroup({
        word: new FormControl('', [Validators.required]),
        associationVariants: new FormArray([
          new FormControl('', [Validators.required]),
          new FormControl('', [Validators.required]),
          new FormControl('', [Validators.required]),
          new FormControl('', [Validators.required]),
        ]),
      })
    );
  }
  removeAssociation(i: number) {
    this.associations.removeAt(i);
    this.associations.patchValue([]);
  }

  setStep(index: number): void {
    this.step = index;
  }
  nextStep(): void {
    this.step++;
  }
  prevStep(): void {
    this.step--;
  }
}
