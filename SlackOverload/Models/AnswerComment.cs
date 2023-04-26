using System.ComponentModel.DataAnnotations;

namespace SlackOverload.Models
{
    public class AnswerComment
    {
        public int Id { get; set; }

        public int AnswerId { get; set; }

        public Answer Answer { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string Comment { get; set; }

        public AnswerComment(Answer answer, string comment)
        {
           
            AnswerId = answer.Id;
            Answer = answer;

            Comment = comment;
        }

        public AnswerComment() { }

    }
}
