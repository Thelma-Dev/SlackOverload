using SlackOverload.Areas.Identity.Data;

namespace SlackOverload.Models
{
    public class QuestionVote
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int QuestionId { get; set; }

        public Question? Question { get; set; }

        public bool UpVote { get; set; }


        public QuestionVote() { }

    }
}
