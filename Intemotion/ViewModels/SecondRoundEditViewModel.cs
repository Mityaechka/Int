using Intemotion.Entities.Rounds.SecondRound;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class SecondRoundViewModel
    {
        public SecondRoundViewModel(SecondRound model)
        {
            if (model != null)
            {
                Id = model.Id;
                if (model.Questions != null)
                    Questions = model.Questions.Select(x => new TruthQuestionViewModel(x)).ToList();
            }
        }
        public int Id { get; set; }
        public List<TruthQuestionViewModel> Questions { get; set; } = new List<TruthQuestionViewModel>();
    }
    public class TruthQuestionViewModel
    {
        public TruthQuestionViewModel(TruthQuestion model)
        {
            Id = model.Id;
            Value = model.Value;
            IsTruth = model.IsTruth;
            Index = model.Index;
        }
        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsTruth { get; set; }
        public int Index { get; set; }
    }
    public class SecondRoundEditViewModel
    {
        public List<TruthQuestionEditViewModel> Questions { get; set; }
    }
    public class TruthQuestionEditViewModel
    {
        public string Value { get; set; }
        public bool IsTruth { get; set; }
        public int Index { get; set; }
    }
}
