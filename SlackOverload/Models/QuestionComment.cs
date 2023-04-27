using System.ComponentModel.DataAnnotations;

namespace SlackOverload.Models
{
    public class QuestionComment
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        [Required(AllowEmptyStrings =false)]
        public string Comment { get; set; }

        public DateTime DatePosted { get; set; }

        public QuestionComment(Question question, string comment)
        {
            Question = question;
            QuestionId = question.Id;

            Comment = comment;
        }

        public QuestionComment() { }
    }
}
