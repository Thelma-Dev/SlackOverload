namespace SlackOverload.Models
{
    public class QuestionTag
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public int TagId { get; set; }

        public Tags Tags { get; set; }

        public QuestionTag() { }
    }
}
