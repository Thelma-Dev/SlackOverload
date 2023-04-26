using SlackOverload.Areas.Identity.Data;

namespace SlackOverload.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public virtual HashSet<Answer> Answers { get; set; } = new HashSet<Answer>();

        public virtual HashSet<QuestionComment> QuestionComments { get; set; } = new HashSet<QuestionComment>();

        public virtual HashSet<QuestionTag> QuestionTags { get; set; } = new HashSet<QuestionTag>();

        public virtual HashSet<QuestionVote> QuestionVotes { get; set; } = new HashSet<QuestionVote>();

        public Question(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public Question() { }
    }
}
