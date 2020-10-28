using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class FirstRoundEditViewModel
    {
        public virtual List<QuestionsCategoryEditViewModel> QuestionsCategories { get; set; } = new List<QuestionsCategoryEditViewModel>();
        public virtual List<int> QuestionCosts { get; set; } = new List<int>();
    }
    public class QuestionsCategoryEditViewModel
    {
        public string Name { get; set; }
        public virtual List<IntellectualQuestionEditViewModel> Questions { get; set; } = new List<IntellectualQuestionEditViewModel>();
    }
    public class IntellectualQuestionCostEditViewModel
    {
        public int Index { get; set; }
        public int Value { get; set; }
    }
    public class IntellectualQuestionEditViewModel
    {
        public string Value { get; set; }
        public virtual List<string> Answers { get; set; } = new List<string>();
        public int Index { get; set; }
    }
    public class IntellectualAnswerEditViewModel
    {
        public string Value { get; set; }
    }
}
