namespace SlackOverload.Models
{
    public class MarkedAnswers
    {
        public int id { get; set; }

        public int AnswerId { get; set; }

        public Answer Answer { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public bool IsCorrect { get; set; } = true;
    }
}
