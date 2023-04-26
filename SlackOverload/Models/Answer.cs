using System.ComponentModel.DataAnnotations;

namespace SlackOverload.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public Question? Question { get; set; }

        [Required(AllowEmptyStrings =false)]
        [Display(Name ="Answer")]
        public string QuestionAnswer { get; set; }

        public virtual HashSet<AnswerComment> AnswerComments { get; set; } = new HashSet<AnswerComment>();

        public virtual HashSet<AnswerVote> AnswerVotes { get; set; } = new HashSet<AnswerVote>();

        public Answer(Question question, string answer)
        {
            Question = question;
            QuestionId = question.Id;

            QuestionAnswer = answer;
        }

        public Answer() { }


    }
}
