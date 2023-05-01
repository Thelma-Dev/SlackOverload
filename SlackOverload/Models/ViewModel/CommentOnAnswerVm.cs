using System.ComponentModel.DataAnnotations;

namespace SlackOverload.Models.ViewModel
{
    public class CommentOnAnswerVm
    {
        public int AnswerId { get; set; }

        public virtual Answer? Answer { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public CommentOnAnswerVm() { }

    }
}
