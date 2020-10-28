import { FirstRound } from './../../../../entities/first-round.entity';
import { Component, OnInit, Input } from '@angular/core';
`
"thirdRound": {
  "chronologyAnswer": [
    {
      "userId": "0cb43a5c-6841-4006-a7c5-874c08f98637",
      "answers": [
        {
          "score": 5,
          "isCorrect": true,
          "chronologyId": null
        }
      ]
    }
  ],
  "associationAnswers": [],
  "melodyGuessAnswers": []
}
`;

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css'],
})
export class StatisticComponent implements OnInit {
  @Input() statistic: any;
  scores: any[] = [];
  constructor() {}

  ngOnInit(): void {
    //this.statistic = JSON.parse(statistic);
    this.statistic.users
      //.filter((x) => x.id === 4909)
      .forEach((x) => {
        this.scores.push({
          userId: x.id,
          nickname: x.nickname,
          score: {
            firstRound: this.scoreFirstRound(x.id),
            secondRound: this.scoreSecondRound(x.id),
            thirdRound: this.scoreThirdRound(x.id),
            total: this.score(x.id),
          },
        });
      });
    this.scores.sort((a, b) => a.score.tota - b.score.total);
  }
  score(userId: string) {
    return (
      this.scoreFirstRound(userId) +
      this.scoreSecondRound(userId) +
      this.scoreThirdRound(userId)
    );
  }
  scoreFirstRound(userId: string) {
    return this.statistic.firstRound
      .filter((x) => x.userId === userId)
      .flatMap((x) => x.answers)
      .reduce((a, b) => (a += b.score), 0);
  }
  scoreSecondRound(userId: string) {
    return this.statistic.secondRound
      .filter((x) => x.userId === userId)
      .flatMap((x) => x.answers)
      .reduce((a, b) => (a += b.score), 0);
  }
  scoreThirdRound(userId: string) {
    const thirdRound = this.statistic.thirdRound;
    if (thirdRound === undefined) {
      return 0;
    }

    return (
      thirdRound.chronologyAnswer
        .filter((x) => x.userId === userId)
        .flatMap((x) => x.answers)
        .reduce((a, b) => (a += b.score), 0) +
      thirdRound.associationAnswers
        .filter((x) => x.userId === userId)
        .flatMap((x) => x.answers)
        .reduce((a, b) => (a += b.score), 0) +
      thirdRound.melodyGuessAnswers
        .filter((x) => x.userId === userId)
        .flatMap((x) => x.answers)
        .reduce((a, b) => (a += b.score), 0)
    );
  }
}

const statistic = `{
  "users": [
    {
      "id": 4912,
      "nickname": "test@test.com"
    },
    {
      "id": 4765,
      "nickname": "test@test.com"
    },
    {
      "id": 4766,
      "nickname": "test@test.com"
    },
    {
      "id": 4768,
      "nickname": "test@test.com"
    },
    {
      "id": 4770,
      "nickname": "test@test.com"
    },
    {
      "id": 4793,
      "nickname": "test@test.com"
    },
    {
      "id": 4799,
      "nickname": "test@test.com"
    },
    {
      "id": 4800,
      "nickname": null
    },
    {
      "id": 4801,
      "nickname": "test@test.com"
    },
    {
      "id": 4802,
      "nickname": "test@test.com"
    },
    {
      "id": 4803,
      "nickname": null
    },
    {
      "id": 4804,
      "nickname": "test@test.com"
    },
    {
      "id": 4805,
      "nickname": "test@test.com"
    },
    {
      "id": 4806,
      "nickname": "test@test.com"
    },
    {
      "id": 4819,
      "nickname": "test@test.com"
    },
    {
      "id": 4820,
      "nickname": null
    },
    {
      "id": 4821,
      "nickname": "test@test.com"
    },
    {
      "id": 4822,
      "nickname": "test@test.com"
    },
    {
      "id": 4823,
      "nickname": "test@test.com"
    },
    {
      "id": 4824,
      "nickname": "test@test.com"
    },
    {
      "id": 4825,
      "nickname": "test@test.com"
    },
    {
      "id": 4828,
      "nickname": "test@test.com"
    },
    {
      "id": 4829,
      "nickname": "test@test.com"
    },
    {
      "id": 4832,
      "nickname": "test@test.com"
    },
    {
      "id": 4833,
      "nickname": "test@test.com"
    },
    {
      "id": 4835,
      "nickname": "test@test.com"
    },
    {
      "id": 4836,
      "nickname": "test@test.com"
    },
    {
      "id": 4837,
      "nickname": "test@test.com"
    },
    {
      "id": 4838,
      "nickname": "test@test.com"
    },
    {
      "id": 4839,
      "nickname": "test@test.com"
    },
    {
      "id": 4840,
      "nickname": "test@test.com"
    },
    {
      "id": 4841,
      "nickname": "test@test.com"
    },
    {
      "id": 4842,
      "nickname": null
    },
    {
      "id": 4843,
      "nickname": "test@test.com"
    },
    {
      "id": 4846,
      "nickname": "test@test.com"
    },
    {
      "id": 4869,
      "nickname": "test@test.com"
    },
    {
      "id": 4871,
      "nickname": "test@test.com"
    },
    {
      "id": 4875,
      "nickname": "test@test.com"
    },
    {
      "id": 4876,
      "nickname": "test@test.com"
    },
    {
      "id": 4877,
      "nickname": "test@test.com"
    },
    {
      "id": 4880,
      "nickname": "test@test.com"
    },
    {
      "id": 4884,
      "nickname": "test@test.com"
    },
    {
      "id": 4885,
      "nickname": "test@test.com"
    },
    {
      "id": 4886,
      "nickname": "test@test.com"
    },
    {
      "id": 4887,
      "nickname": "test@test.com"
    },
    {
      "id": 4892,
      "nickname": "test@test.com"
    },
    {
      "id": 4893,
      "nickname": "test@test.com"
    },
    {
      "id": 4894,
      "nickname": "test@test.com"
    },
    {
      "id": 4895,
      "nickname": "test@test.com"
    },
    {
      "id": 4896,
      "nickname": "test@test.com"
    },
    {
      "id": 4897,
      "nickname": "test@test.com"
    },
    {
      "id": 4898,
      "nickname": "test@test.com"
    },
    {
      "id": 4901,
      "nickname": "test@test.com"
    },
    {
      "id": 4902,
      "nickname": "test@test.com"
    },
    {
      "id": 4903,
      "nickname": "test@test.com"
    },
    {
      "id": 4904,
      "nickname": "test@test.com"
    },
    {
      "id": 4906,
      "nickname": null
    },
    {
      "id": 4907,
      "nickname": null
    },
    {
      "id": 4909,
      "nickname": "test@test.com"
    }
  ],
  "firstRound": [
    {
      "userId": "0cb43a5c-6841-4006-a7c5-874c08f98637",
      "answers": [
        {
          "isCorrect": true,
          "score": 15,
          "questionId": 2497
        },
        {
          "isCorrect": true,
          "score": 25,
          "questionId": 2498
        },
        {
          "isCorrect": false,
          "score": 0,
          "questionId": 2500
        },
        {
          "isCorrect": true,
          "score": 15,
          "questionId": 2501
        },
        {
          "isCorrect": false,
          "score": 0,
          "questionId": 2502
        },
        {
          "isCorrect": true,
          "score": 35,
          "questionId": 2503
        },
        {
          "isCorrect": true,
          "score": 45,
          "questionId": 2504
        },
        {
          "isCorrect": true,
          "score": 25,
          "questionId": 2506
        },
        {
          "isCorrect": true,
          "score": 45,
          "questionId": 2508
        },
        {
          "isCorrect": false,
          "score": 0,
          "questionId": 2510
        },
        {
          "isCorrect": true,
          "score": 35,
          "questionId": 2511
        },
        {
          "isCorrect": true,
          "score": 45,
          "questionId": 2512
        }
      ]
    }
  ],
  "secondRound": [
    {
      "userId": "0cb43a5c-6841-4006-a7c5-874c08f98637",
      "answers": [
        {
          "isCorrect": true,
          "score": 5,
          "questionId": 1029
        },
        {
          "isCorrect": true,
          "score": 5,
          "questionId": 1030
        },
        {
          "isCorrect": false,
          "score": 0,
          "questionId": 1031
        }
      ]
    }
  ],
  "thirdRound": {
    "chronologyAnswer": [
      {
        "userId": "0cb43a5c-6841-4006-a7c5-874c08f98637",
        "answers": [
          {
            "score": 5,
            "isCorrect": true,
            "chronologyId": null
          }
        ]
      }
    ],
    "associationAnswers": [],
    "melodyGuessAnswers": []
  }
}`;
