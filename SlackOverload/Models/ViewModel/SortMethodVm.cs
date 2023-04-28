using System.ComponentModel.DataAnnotations;

namespace SlackOverload.Models.ViewModel
{
    public class SortMethodVm
    {

        public HashSet<Question> Questions { get; set; } = new HashSet<Question>();

        public QuestionSortMethod? SortMethod { get; set; }

        public enum QuestionSortMethod
        {
            [Display(Name="Earliest")]
            Earliest = 1,

            [Display(Name = "Latest")]
            Latest = 2,

            [Display(Name = "Most Answered")]
            MostAnswered,

            [Display(Name = "Least Answered")]
            LeastAnswered
        }

        public SortMethodVm(HashSet<Question> questions, QuestionSortMethod sortMethod)
        {
            Questions = questions ;
            SortMethod = sortMethod;
        }
        public SortMethodVm() { }
    }
}
