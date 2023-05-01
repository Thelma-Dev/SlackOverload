using System.ComponentModel.DataAnnotations;

namespace SlackOverload.Models.ViewModel
{
    public class CommentOnQuestionVm
    {
        public int QuestionId { get; set; }

        public virtual Question? Question { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public CommentOnQuestionVm() { }

    }
}
