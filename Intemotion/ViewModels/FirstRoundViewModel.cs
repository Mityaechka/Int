using Intemotion.Entities.Rounds.FirstRound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class FirstRoundViewModel
    {
        public FirstRoundViewModel(FirstRound model)
        {
            if (model != null)
            {
                Id = model.Id;
                QuestionsCategories = model.QuestionsCategories.Select(x => new QuestionsCategoryViewModel(x)).ToList();
                //QuestionCosts = model.QuestionCosts.Select(x => new IntellectualQuestionCostViewModel(x)).ToList();
            }
        }
        public int Id { get; set; }
        public virtual List<QuestionsCategoryViewModel> QuestionsCategories { get; set; }
        //public virtual List<IntellectualQuestionCostViewModel> QuestionCosts { get; set; }
    }
    public class QuestionsCategoryViewModel
    {
        public QuestionsCategoryViewModel(QuestionsCategory model)
        {
            if (model != null)
            {
                Id = model.Id;
                Name = model.Name;
                Questions = model.Questions.Select(x => new IntellectualQuestionViewModel(x)).ToList();
            }
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<IntellectualQuestionViewModel> Questions { get; set; }
    }
    public class IntellectualQuestionCostViewModel
    {
        public IntellectualQuestionCostViewModel(IntellectualQuestionCost model)
        {
            if (model != null)
            {
                Id = model.Id;
                Index = model.Index;
                Value = model.Value;
            }
        }
        public int Id { get; set; }
        public int Index { get; set; }
        public int Value { get; set; }
    }
    public class IntellectualQuestionViewModel
    {
        public IntellectualQuestionViewModel(IntellectualQuestion model)
        {
            if (model != null)
            {
                Id = model.Id;
                Value = model.Value;
                Index = model.Index;
                Answers = model.Answers.Select(x => new IntellectualAnswerViewModel(x)).ToList();
            }
        }
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual List<IntellectualAnswerViewModel> Answers { get; set; }
        public int Index { get; set; }
    }
    public class IntellectualAnswerViewModel
    {
        public IntellectualAnswerViewModel(IntellectualAnswer model)
        {
            if (model != null)
            {
                Id = model.Id;
                Value = model.Value;
                Index = model.Index;
            }
        }
        public int Id { get; set; }
        public string Value { get; set; }
        public int Index { get; set; }
    }
}
