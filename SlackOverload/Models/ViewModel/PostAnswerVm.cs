using SlackOverload.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace SlackOverload.Models.ViewModel
{
    public class PostAnswerVm
    {
        public int QuestionId { get; set; }

        public virtual Question? Question { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Answer")]
        public string QuestionAnswer { get; set; }

        public PostAnswerVm() { }

    }
}
