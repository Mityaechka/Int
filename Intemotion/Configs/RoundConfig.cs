using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Configs
{
    public class RoundConfig
    {
        public int FirstRoundReceivingAnswersInterval { get; set; }
        public int FirstRoundSelectQuestionInterval { get; set; }

        public int FirstRoundBigScoreTime { get; set; }
        public int FirstRoundSmallScoreTime { get; set; }

        public int FirstRoundBigScoreValue { get; set; }
        public int FirstRoundSmallScoreValue { get; set; }

        public int[] FirstRoundQuestionCost { get; set; }
        public int FirstRoundAdInterval { get; set; }
        public int SecondRoundCorrectAnswerScore { get; set; }
        public int SecondRoundSelectQuestionInterval { get; set; }
        public int SecondRoundReceivingAnswersInterval { get; set; }




        public int ThirdRoundReceivingAnswersInterval { get; set; }
        public int ThirdRoundSelectQuestionInterval { get; set; }
        public int ThirdRoundChronologyScore { get; set; }
        public int ThirdRoundMelodyScore { get; set; }
        public int ThirdRoundAssociationScore { get; set; }
    }
}
