using SlackOverload.Areas.Identity.Data;

namespace SlackOverload.Models
{
    public class AnswerVote
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }

        public bool UpVote { get; set; }

        public AnswerVote() { }
    }
}
